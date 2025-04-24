using Application.Interfaces;
using AutoMapper;
using Domain.Models;
using Infrastructure.Interfaces;

namespace Application.Services
{
    public class SetorService : ISetorService
    {
        private readonly ISetorRepository _setorRepository;
        private readonly IMapper _mapper;
        public Task<bool> AdicionarSetoresAsync(Setor setor)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AtualizarSetoresAsync(Setor setor)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Setor>> BuscarSetoresAsync()
        {
            throw new NotImplementedException();
        }

        public Task<RetornoPaginado<Setor>> BuscarSetoresPaginadoAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Setor> BuscarSetorPorIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeletarSetoresAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
