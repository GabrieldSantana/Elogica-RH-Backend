using Application.Interfaces;
using Domain.Models;
using Infrastructure.Interfaces;

namespace Application.Services
{
    public class CargosSetoresService : ICargosSetoresService
    {

        private readonly ICargosSetoresRepository _cargosSetoresRepository;

        public CargosSetoresService(ICargosSetoresRepository cargosSetoresRepository)
        {
            _cargosSetoresRepository = cargosSetoresRepository;
        }

        public async Task<int> AdicionarCargosSetoresAsync(CargosSetores cargosSetores)
        {
            try
            {
                if (cargosSetores.CargosId <= 0 || cargosSetores.SetoresId <= 0)
                {
                    throw new Exception("Id deve ser positivo e maior que zero");
                }

                var adicionarCargosSetores = await _cargosSetoresRepository.AdicionarCargosSetoresAsync(cargosSetores);

                return adicionarCargosSetores;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> AtualizarCargosSetoresAsync(CargosSetores cargosSetores)
        {
            try
            {
                if(cargosSetores.CargosId <=0  || cargosSetores.SetoresId <= 0)
                {
                    throw new Exception("Id deve ser positivo e maior que zero");
                }

                var atualizarCargosSetores = await _cargosSetoresRepository.AtualizarCargosSetoresAsync(cargosSetores);

                return atualizarCargosSetores;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<CargosSetores>> BuscarCargosSetoresAsync()
        {
            try
            {
                var buscarCargosSetores = await _cargosSetoresRepository.BuscarCargosSetoresAsync();

                return buscarCargosSetores;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<RetornoPaginado<CargosSetores>> BuscarCargosSetoresPaginadoAsync(int quantidade, int pagina)
        {
            try
            {
                

                var cargosSetoresRetornoPaginado = await _cargosSetoresRepository.BuscarCargosSetoresPaginadoAsync(quantidade, pagina);
                return cargosSetoresRetornoPaginado;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Task<bool> ExcluirCargosSetoresAsync(CargosSetores cargosSetores)
        {
            throw new NotImplementedException();
        }
    }
}
