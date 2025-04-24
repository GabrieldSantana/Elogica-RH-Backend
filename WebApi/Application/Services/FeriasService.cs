
using System.ComponentModel.DataAnnotations;
using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain;
using Infrastructure.Interfaces;

namespace Application.Services
{
    public class FeriasService : IFeriasService
    {
        private readonly IFeriasRepository _feriasRepository;
        private readonly IFuncionarioRepository _funcionarioRepository;
        private readonly IMapper _mapper;

        public FeriasService(
            IFeriasRepository feriasRepository,
            IFuncionarioRepository funcionarioRepository,
            IMapper mapper)
        {
            _feriasRepository = feriasRepository;
            _funcionarioRepository = funcionarioRepository;
            _mapper = mapper;
        }
        public async Task<RetornoPaginado<Ferias>> BuscarFeriasPaginadoAsync(int pagina, int quantidade)
        {
            var resultado = await _feriasRepository.BuscarFeriasPaginadoAsync(pagina, quantidade);

            return new RetornoPaginado<FeriasDto>
            {
                TotalRegistro = resultado.TotalRegistro,
                Quantidade = resultado.Quantidade,
                Pagina = resultado.Pagina,
                RetornoPagina = _mapper.Map<List<Ferias>>(resultado.RetornoPagina)
            };
        }
        public async Task<IEnumerable<Ferias>> BuscarFeriasAsync()
        {
            var ferias = await _feriasRepository.BuscarFeriasAsync();
            return _mapper.Map<IEnumerable<Ferias>>(ferias);
        }

        public async Task<Ferias> BuscarFeriasPorIdAsync(int id)
        {
            var ferias = await _feriasRepository.BuscarFeriasPorIdAsync(id);
            return _mapper.Map<Ferias>(ferias);
        }

        public async Task<Ferias> AdicionarFeriasAsync(Ferias dto)
        {
            try
            {

                var funcionario = await _funcionarioRepository.BuscarFeriasPorIdAsync(dto.FuncionarioId);

                if (funcionario == null)
                {
                    throw new ValidationException($"Não foi possível encontrar um funcionário com o ID {dto.FuncionarioId}");
                }
                var ferias = _mapper.Map<Ferias>(dto);

                await ValidarRegrasFerias(ferias, funcionario);


                var feriasCriada = await _feriasRepository.BuscarFeriasAsync(ferias);

                return _mapper.Map<Ferias>(feriasCriada);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao criar férias: {ex.Message}");
                throw;
            }
        }
        public async Task AtualizarFeriasAsync(int id, Ferias dto)
        {
            try
            {
                Console.WriteLine($"Buscando funcionário com ID: {dto.FuncionarioId}");
                var ferias = _mapper.Map<Ferias>(dto);
                ferias.Id = id;

                var feriasExistente = await _feriasRepository.BuscarFeriasPorIdAsync(ferias.Id);
                if (feriasExistente == null)
                {
                    throw new KeyNotFoundException($"Férias com ID {ferias.Id} não encontradas");
                }

                var funcionario = await _funcionarioRepository.ObterPorIdAsync(ferias.FuncionarioId);
                Console.WriteLine($"Funcionário encontrado: {funcionario?.Id} - {funcionario?.Nome}");

                if (funcionario == null)
                {

                    var funcionarioExiste = await _funcionarioRepository.FuncionarioExiste(ferias.FuncionarioId);
                    Console.WriteLine($"Verificação direta no banco: {funcionarioExiste}");

                    if (!funcionarioExiste)
                    {
                        throw new ValidationException($"Funcionário com ID {ferias.FuncionarioId} não encontrado no banco de dados");
                    }
                    else
                    {
                        throw new ValidationException($"Funcionário com ID {ferias.FuncionarioId} existe, mas não pôde ser carregado");
                    }
                }

                await ValidarRegrasFerias(ferias, funcionario);


                await _feriasRepository.AtualizarFeriasAsync(ferias);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERRO ao realizar atuzalização: {ex}");
                throw;
            }
        }
        public async Task ExcluirFeriasAsync(int id)
        {
            await _feriasRepository.ExcluirFeriasAsync(id);
        }

        private async Task ValidarRegrasFerias(Ferias ferias, Funcionario funcionario)
        {
            // 1. Validação básica de datas
            if (ferias.DataFim <= ferias.DataInicio)
            {
                throw new ValidationException("A data final deve ser maior que a data inicial");
            }

            // 2. Verificar antecedência mínima de 1 mês
            var hoje = DateTime.Today;
            var umMesAFrente = hoje.AddMonths(1);

            if (ferias.DataInicio < umMesAFrente)
            {
                throw new ValidationException($"As férias devem ser agendadas com pelo menos 1 mês de antecedência. Data mínima permitida: {umMesAFrente:dd/MM/yyyy}");
            }

            // 3. Verificar tempo mínimo de empresa (1 ano)
            if (funcionario.DataContratacao.AddYears(1) > hoje)
            {
                throw new ValidationException($"O funcionário {funcionario.Nome} foi contratado em {funcionario.DataContratacao:dd/MM/yyyy} " +
                    $"e ainda não completou 1 ano de empresa.");
            }

            // 4. Verificar conflitos de período
            if (await _feriasRepository.FuncionarioTemFerias(ferias.FuncionarioId, ferias.DataInicio, ferias.DataFim))
            {
                var feriasExistentes = await ObterFeriasDoFuncionarioNoAno(ferias.FuncionarioId, ferias.DataInicio.Year);
                var periodos = feriasExistentes.Select(f => $"{f.DataInicio:dd/MM/yyyy} a {f.DataFim:dd/MM/yyyy}");

                throw new ValidationException($"Já existem férias cadastradas para este funcionário nos seguintes períodos: {string.Join(", ", periodos)}");
            }

            // 5. Verificar limite de dias e períodos
            var feriasDoAno = await ObterFeriasDoFuncionarioNoAno(ferias.FuncionarioId, ferias.DataInicio.Year);
            var diasSolicitados = (ferias.DataFim - ferias.DataInicio).Days + 1;

            if (feriasDoAno.Count() >= 3)
            {
                throw new ValidationException("Limite máximo de 3 períodos de férias por ano já foi atingido");
            }

            var totalDias = feriasDoAno.Sum(f => (f.DataFim - f.DataInicio).Days + 1) + diasSolicitados;
            if (totalDias > 30)
            {
                throw new ValidationException($"O total de dias de férias não pode exceder 30 dias por ano. Dias já agendados: {totalDias - diasSolicitados}");
            }
        }
        private async Task<IEnumerable<Ferias>> ObterFeriasDoFuncionarioNoAno(int funcionarioId, int ano)
        {
            var todasFerias = await _feriasRepository.BuscarFeriasAsync();
            return todasFerias
                .Where(f => f.FuncionarioId == funcionarioId &&
                           f.DataInicio.Year == ano)
                .ToList();
        }
    }
}