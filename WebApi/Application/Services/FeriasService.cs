
using System.ComponentModel.DataAnnotations;
using Domain.Models;
using Application.Interfaces;
using AutoMapper;
using Domain;
using Infrastructure.Interfaces;
using Domain.Dtos;

namespace Application.Services
{
    public class FeriasService : IFeriasService
    {
        private readonly IFeriasRepository _feriasRepository;
        private readonly IMapper _mapper;
        private readonly IFuncionarioService _funcionarioService;

        public FeriasService(IFeriasRepository feriasRepository, IMapper mapper, IFuncionarioService funcionarioService)
        {
            _feriasRepository = feriasRepository;
            _mapper = mapper;
            _funcionarioService = funcionarioService;
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
        public async Task<Ferias> AdicionarFeriasAsync(Ferias ferias)
        {
            try
            {

                var funcionario = await _funcionarioService.BuscarFuncionarioPorIdAsync(ferias.FuncionarioId);
                if (funcionario == null)
                    throw new ValidationException("Funcionário não encontrado.");

                await ValidarRegrasFerias(ferias, funcionario);

                return await _feriasRepository.AdicionarFeriasAsync(ferias);
            }
            catch
            {
                throw new Exception("Não foi possível adicionar férias");
            }
        }
        public async Task AtualizarFeriasAsync(int id, FeriasDto dto)
        {
            try
            {
                ;
                var ferias = _mapper.Map<Ferias>(dto);
                ferias.Id = id;

                if (ferias == null)
                {
                    throw new KeyNotFoundException($"Férias com ID {ferias.Id} não encontradas");
                }

                var funcionario = await _funcionarioService.BuscarFuncionarioPorIdAsync(ferias.FuncionarioId);

                if (funcionario == null)
                {

                        throw new ValidationException($"Funcionário com ID {ferias.FuncionarioId} não encontrado no banco de dados");
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
   

        }

        private async Task<IEnumerable<Ferias>> ObterFeriasDoFuncionarioNoAno(int funcionarioId, int ano)
        {
            var todasFerias = await _feriasRepository.BuscarFeriasAsync();
            return todasFerias
                .Where(f => f.FuncionarioId == funcionarioId &&
                           f.DataInicio.Year == ano)
                .ToList();
        }
        private async Task<IEnumerable<Ferias>> ObterFeriasDoFuncionarioNoPeriodo(int funcionarioId, DateTime dataInicio, DateTime dataFim)
        {
            var todasFerias = await _feriasRepository.BuscarFeriasAsync();
            return todasFerias
                .Where(f => f.FuncionarioId == funcionarioId &&
                           f.DataInicio >= dataInicio &&
                           f.DataFim <= dataFim)
                .ToList();
        }

    }
}