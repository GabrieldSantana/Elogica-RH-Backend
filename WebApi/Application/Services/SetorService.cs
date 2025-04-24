using Application.Interfaces;
using AutoMapper;
using Domain.Dtos;
using Domain.Models;
using Infrastructure.Interfaces;

namespace Application.Services
{
    public class SetorService : ISetorService
    {
        private readonly ISetorRepository _setorRepository;
        private readonly IMapper _mapper;

        public SetorService(ISetorRepository setorRepository, IMapper mapper)
        {
            _setorRepository = setorRepository;
            _mapper = mapper;
        }

        public async Task<bool> AdicionarSetoresAsync(SetorDto setorDto)
        {
            var setor = _mapper.Map<Setor>(setorDto);
            return await _setorRepository.AdicionarSetoresAsync(setor);
        }

        public async Task<bool> AtualizarSetoresAsync(int id, SetorDto setorDto)
        {
            var setor = _mapper.Map<Setor>(setorDto);
            setor.Id = id; 
            return await _setorRepository.AtualizarSetoresAsync(setor);
        }

        public async Task<IEnumerable<Setor>> BuscarSetoresAsync()
        {
            return await _setorRepository.BuscarSetoresAsync();
        }

        public async Task<RetornoPaginado<Setor>> BuscarSetoresPaginadoAsync(int pagina, int quantidade)
        {
            return await _setorRepository.BuscarSetoresPaginadoAsync(pagina, quantidade);
        }

        public async Task<Setor> BuscarSetorPorIdAsync(int id)
        {
            return await _setorRepository.BuscarSetoresPorIdAsync(id);
        }

        public async Task<bool> ExcluirSetoresAsync(int id)
        {
            return await _setorRepository.ExcluirSetoresAsync(id);
        }
    }
}
