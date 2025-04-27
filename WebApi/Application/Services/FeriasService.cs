using System.ComponentModel.DataAnnotations;
using Domain.Models;
using Application.Interfaces;
using AutoMapper;
using Infrastructure.Interfaces;
using Domain.Dtos;
using Domain.Notificacao;

namespace Application.Services
{
    public class FeriasService : IFeriasService
    {
        private readonly IFeriasRepository _feriasRepository;
        private readonly IMapper _mapper;
        private readonly IFuncionarioService _funcionarioService;
        private readonly INotificador _notificador;

        public FeriasService(
            IFeriasRepository feriasRepository,
            IMapper mapper,
            IFuncionarioService funcionarioService,
            INotificador notificador)
        {
            _feriasRepository = feriasRepository;
            _mapper = mapper;
            _funcionarioService = funcionarioService;
            _notificador = notificador;
        }

        public async Task<RetornoPaginado<Ferias>> BuscarFeriasPaginadoAsync(int pagina, int quantidade)
        {
            try
            {
                if (pagina < 1 || quantidade < 1)
                    throw new ValidationException("Página e quantidade devem ser maiores que zero.");

                var resultado = await _feriasRepository.BuscarFeriasPaginadoAsync(pagina, quantidade);

                return new RetornoPaginado<Ferias>
                {
                    TotalRegistro = resultado.TotalRegistro,
                    Registros = resultado.Registros
                };
            }
            catch (Exception)
            {
                throw new Exception("Não foi possível realizar retorno paginado");
            }
        }
        public async Task<IEnumerable<Ferias>> BuscarFeriasAsync()
        {
            try
            {
                var ferias = await _feriasRepository.BuscarFeriasAsync();
                return _mapper.Map<IEnumerable<Ferias>>(ferias);
            }
            catch (Exception)
            {
                throw new Exception("Não foi possível realizar a busca de férias");
            }
        }
        public async Task<Ferias> BuscarFeriasPorIdAsync(int id)
        {
            try
            {
                var ferias = await _feriasRepository.BuscarFeriasPorIdAsync(id);
                return _mapper.Map<Ferias>(ferias);
            }
            catch (Exception)
            {
                throw new Exception("Não foi possível realizar a busca de por ID");
            }
        }

        public async Task<Ferias> AtualizarFeriasAsync(int id, FeriasDto dto)
        {
            try
            {
                var ferias = _mapper.Map<Ferias>(dto);
                ferias.Id = id;

                //Verifique se as férias existem
                var feriasExistente = await _feriasRepository.BuscarFeriasPorIdAsync(id);
                if (feriasExistente == null)
                {
                    _notificador.Handle(new Notificacao("Férias não encontradas"));
                    return null;
                }

                //Validações
                if (ferias.DataFim <= ferias.DataInicio)
                {
                    _notificador.Handle(new Notificacao("Data final deve ser maior que a data inicial"));
                    return null;
                }

                //Atualize e retorne as férias atualizadas
                await _feriasRepository.AtualizarFeriasAsync(ferias);
                return await _feriasRepository.BuscarFeriasPorIdAsync(id);
            }
            catch (Exception ex)
            {
                _notificador.Handle(new Notificacao($"Erro ao atualizar férias: {ex.Message}"));
                return null;
            }
        }
        public async Task<(bool Success, string Message)> ExcluirFeriasAsync(int id)
        {
            try
            {
                //Verifica se as férias existem
                var feriasExistente = await BuscarFeriasPorIdAsync(id);
                if (feriasExistente == null)
                {
                    return (false, $"Não foi possível encontrar férias com o ID {id}");
                }

                await _feriasRepository.ExcluirFeriasAsync(id);
                return (true, "Férias excluídas com sucesso");
            }
            catch (Exception ex)
            {
                return (false, $"Falha ao excluir férias ID {id}: {ex.Message}");
            }
        }
        public async Task<Ferias> AdicionarFeriasAsync(Ferias ferias)
        {
            try
            {
                var funcionario = await _funcionarioService.BuscarFuncionarioPorIdAsync(ferias.FuncionarioId);
                if (funcionario == null)
                {
                    _notificador.Handle(new Notificacao("Funcionário não encontrado."));
                    return null;
                }

                //Adicionando false para isEdicao e null para feriasId
                await ValidarRegrasFerias(ferias, funcionario, false, null);

                if (_notificador.TemNotificacao())
                    return null;

                return await _feriasRepository.AdicionarFeriasAsync(ferias);
            }
            catch (Exception ex)
            {
                _notificador.Handle(new Notificacao($"Erro ao adicionar férias: {ex.Message}"));
                return null;
            }
        }
        private async Task ValidarRegrasFerias(Ferias ferias, Funcionario funcionario, bool isEdicao, int? feriasId)
        {
            var diasSolicitados = (ferias.DataFim - ferias.DataInicio).Days + 1;
            var ano = ferias.DataInicio.Year;

            //Obter todas as férias do funcionário no ano
            var feriasDoAno = (await _feriasRepository.BuscarFeriasAsync())
                .Where(f => f.FuncionarioId == ferias.FuncionarioId && f.DataInicio.Year == ano)
                .ToList();

            //Remover o registro atual se for edição
            if (isEdicao && feriasId.HasValue)
            {
                feriasDoAno = feriasDoAno.Where(f => f.Id != feriasId.Value).ToList();
            }

            //Verificar conflitos de período primeiro
            foreach (var periodoExistente in feriasDoAno)
            {
                if ((ferias.DataInicio >= periodoExistente.DataInicio && ferias.DataInicio <= periodoExistente.DataFim) ||
                    (ferias.DataFim >= periodoExistente.DataInicio && ferias.DataFim <= periodoExistente.DataFim) ||
                    (ferias.DataInicio <= periodoExistente.DataInicio && ferias.DataFim >= periodoExistente.DataFim))
                {
                    _notificador.Handle(new Notificacao($"Período de férias já agendado: {periodoExistente.DataInicio:dd/MM/yyyy} a {periodoExistente.DataFim:dd/MM/yyyy}"));
                    return;
                }
            }

            //Verificar limite de períodos (máximo 3)
            if (feriasDoAno.Count >= 3)
            {
                _notificador.Handle(new Notificacao("Limite máximo de 3 períodos de férias por ano já foi atingido"));
                return;
            }

            //Calcular total de dias corretamente
            var totalDiasJaAgendados = feriasDoAno.Sum(f => (f.DataFim - f.DataInicio).Days + 1);

            if (totalDiasJaAgendados >= 30)
            {
                _notificador.Handle(new Notificacao("Limite máximo de 30 dias de férias por ano já foi atingido"));
                return;
            }

            if ((totalDiasJaAgendados + diasSolicitados) > 30)
            {
                _notificador.Handle(new Notificacao($"Esta solicitação de {diasSolicitados} dias excede o limite. Dias já utilizados: {totalDiasJaAgendados}. Dias disponíveis: {30 - totalDiasJaAgendados}"));
                return;
            }

            //Verificação dos 2 anos
            await ValidarPeriodoDoisAnos(funcionario, ferias, isEdicao, feriasId);
        }

        private async Task ValidarPeriodoDoisAnos(Funcionario funcionario, Ferias ferias, bool isEdicao, int? feriasId)
        {
            var doisAnos = funcionario.DataContratacao.AddYears(2);
            if (DateTime.Now < doisAnos)
            {
                var feriasAntesDoisAnos = (await _feriasRepository.BuscarFeriasAsync())
                    .Where(f => f.FuncionarioId == funcionario.Id &&
                               f.DataFim <= doisAnos)
                    .ToList();

                if (isEdicao && feriasId.HasValue)
                {
                    feriasAntesDoisAnos = feriasAntesDoisAnos.Where(f => f.Id != feriasId.Value).ToList();
                }

                var totalDiasAntesDoisAnos = feriasAntesDoisAnos.Sum(f => (f.DataFim - f.DataInicio).Days + 1);
                var diasNecessarios = 30 - totalDiasAntesDoisAnos;

                if (diasNecessarios > 0)
                {
                    _notificador.Handle(new Notificacao($"O funcionário deve tirar {diasNecessarios} dias de férias antes de completar 2 anos na empresa"));
                }
            }
        }
    }
}