using System.Globalization;
using System.Text;
using Application.Interfaces;
using AutoMapper;
using Domain.Dtos;
using Domain.Models;
using Infrastructure.Interfaces;


namespace Application.Services
{
    public class CargosService : ICargosServices
    {
        private readonly ICargosRepository _repository;
        private readonly IMapper _mapper;

        public CargosService(ICargosRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<int> AdicionarCargosAsync(CargosDto cargosDto)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(cargosDto.Titulo))
                {
                    throw new Exception("O título do cargo não pode estar em branco.");
                }

                string tituloPadronizado = RemoverAcentos(cargosDto.Titulo.Trim().ToLowerInvariant());

                var cargosExistentes = await _repository.BuscarCargosAsync();

                bool tituloDuplicado = cargosExistentes
                    .Any(c => RemoverAcentos(c.Titulo.Trim().ToLowerInvariant()) == tituloPadronizado);

                if (tituloDuplicado)
                {
                    throw new Exception("Já existe um cargo com este título.");
                }

                var cargo = new Cargos
                {
                    Titulo = cargosDto.Titulo.Trim(),
                    Descricao = cargosDto.Descricao,
                    SalarioBase = cargosDto.SalarioBase
                };

                return await _repository.AdicionarCargosAsync(cargo);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private string RemoverAcentos(string texto)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(texto))
                    return texto;

                var normalizedString = texto.Normalize(NormalizationForm.FormD);
                var stringBuilder = new StringBuilder();

                foreach (var c in normalizedString)
                {
                    var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                    if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                    {
                        stringBuilder.Append(c);
                    }
                }

                return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<bool> AtualizarCargosAsync(int id,AtualizarCargosDto cargosDto)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(cargosDto.Titulo))
                    throw new Exception("O título do cargo não pode estar em branco.");

                string tituloPadronizado = RemoverAcentos(cargosDto.Titulo.Trim().ToLowerInvariant());
                var cargosExistentes = await _repository.BuscarCargosAsync();

                var cargoExistente = cargosExistentes.FirstOrDefault(c =>
                    RemoverAcentos(c.Titulo.Trim().ToLowerInvariant()) == tituloPadronizado && c.Id != id);

                if (cargoExistente != null)
                    throw new Exception("Já existe outro cargo com este título.");

                var cargoAtualizado = new Cargos
                {
                    Id = id,
                    Titulo = cargosDto.Titulo.Trim(),
                    Descricao = cargosDto.Descricao,
                    SalarioBase = cargosDto.SalarioBase
                };

                return await _repository.AtualizarCargosAsync(cargoAtualizado);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Cargos>> BuscarCargosAsync()
        {
            try
            {
                var cargos = await _repository.BuscarCargosAsync();
                return cargos;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<RetornoPaginado<Cargos>> BuscarCargosPaginadoAsync(int pagina, int quantidade)
        {
            try
            {
                var resultado = await _repository.BuscarCargosPaginadoAsync(pagina, quantidade);
                return resultado;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Cargos> BuscarCargosPorIdAsync(int id)
        {
            try
            {
                var cargo = await _repository.BuscarCargosPorIdAsync(id);
                if (cargo == null)
                {
                    throw new Exception("Cargo não encontrado.");
                }
                return cargo;
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        public async Task<bool> ExcluirCargosAsync(int id)
        {
            try
            {
                var cargo = await _repository.BuscarCargosPorIdAsync(id);
                if (cargo == null)
                    throw new Exception("Cargo não encontrado.");

                return await _repository.ExcluirCargosAsync(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        Task<IEnumerable<CargosDto>> ICargosServices.BuscarCargosAsync()
        {
            throw new NotImplementedException();
        }
    }
};