using Application.Interfaces;
using Domain.Models;
using Infrastructure.Interfaces;

namespace Application.Services;

public class CargosSetoresService : ICargosSetoresService
{

    private readonly ICargosSetoresRepository _cargosSetoresRepository;
    private readonly ICargosServices _cargosServices;
    private readonly ISetorService _setorService;

    public CargosSetoresService(ICargosSetoresRepository cargosSetoresRepository, ICargosServices cargosServices, ISetorService setorService)
    {
        _cargosSetoresRepository = cargosSetoresRepository;
        _cargosServices = cargosServices;
        _setorService = setorService;
    }





    #region Adicionar cargosSetores
    public async Task<int> AdicionarCargosSetoresAsync(CargosSetores cargosSetores)
    {
        try
        {


            if (cargosSetores.CargosId <= 0 )
            {
                throw new Exception("o cargosId deve ser positivo e maior que zero!");
            }
            if( cargosSetores.SetoresId <= 0)
            {
                throw new Exception("o SetoresId deve ser positivo e maior que zero!");
            }

            await _setorService.BuscarSetorPorIdAsync(cargosSetores.SetoresId);

            await _cargosServices.BuscarCargosPorIdAsync(cargosSetores.CargosId);
            var adicionarCargosSetores = await _cargosSetoresRepository.AdicionarCargosSetoresAsync(cargosSetores);

            return adicionarCargosSetores;
        }
        catch (Exception ex)
        {

            throw;
        }
    }
    #endregion

    #region Atualizar cargosSetoes
    public async Task<bool> AtualizarCargosSetoresAsync(CargosSetores cargosSetoresNovo, CargosSetores cargosSetoresAntigo)
    {
        try
        {
            if( cargosSetoresAntigo == null|| cargosSetoresAntigo.CargosId <= 0 || cargosSetoresAntigo.SetoresId <= 0)
            {
                throw new Exception("Os IDs do relacionamento antigo (Cargo e Setor) devem ser positivos e maiores que zero.");
            }

            if (cargosSetoresNovo == null || cargosSetoresNovo.CargosId <= 0 || cargosSetoresNovo.SetoresId <= 0)
            {
                throw new Exception("Os IDs do relacionamento novo (Cargo e Setor) devem ser positivos e maiores que zero.");
            }

            if(cargosSetoresAntigo.SetoresId == cargosSetoresNovo.SetoresId && cargosSetoresAntigo.CargosId == cargosSetoresNovo.CargosId)
            {
                throw new Exception("Os valores do novo relacionamento são iguais aos do relacionamento antigo. Nenhuma atualização é necessária.");

            }


            await _cargosServices.BuscarCargosPorIdAsync(cargosSetoresAntigo.CargosId);
            await _cargosServices.BuscarCargosPorIdAsync(cargosSetoresNovo.CargosId);


            await _setorService.BuscarSetorPorIdAsync(cargosSetoresNovo.SetoresId);
            await _setorService.BuscarSetorPorIdAsync(cargosSetoresAntigo.SetoresId);
      

            

            var atualizarCargosSetores = await _cargosSetoresRepository.AtualizarCargosSetoresAsync(cargosSetoresNovo, cargosSetoresAntigo);

            return atualizarCargosSetores;
        }
        catch (Exception)
        {

            throw;
        }
    }
    #endregion

    #region Buscar cargosSetores
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
    #endregion

    #region Buscar CargosSetores por pagina

    public async Task<RetornoPaginado<CargosSetores>> BuscarCargosSetoresPaginadoAsync(int quantidade, int pagina)
    {
        try
        {
            if(quantidade <= 0)
            {
                throw new ArgumentException("A quantidade de itens por pagina deve ser positiva e maior que zero!");
            }
            if(pagina <= 0)
            {
                throw new ArgumentException("A quantidade de paginas deve ser positiva e maior que zero!");
            }
         
            var cargosSetoresRetornoPaginado = await _cargosSetoresRepository.BuscarCargosSetoresPaginadoAsync(quantidade, pagina);
            return cargosSetoresRetornoPaginado;
        }
        catch (Exception)
        {

            throw;
        }
    }
    #endregion

    #region Excluir cargosSetores
    public async Task<bool> ExcluirCargosSetoresAsync(CargosSetores cargosSetores)
    {
        try
        {

            if(cargosSetores.CargosId <=0 || cargosSetores.SetoresId <= 0)
            {
                throw new Exception("Os Id fornecido devem ser positivos e maior que zero");
            }

            await _cargosServices.BuscarCargosPorIdAsync(cargosSetores.CargosId);
            await _setorService.BuscarSetorPorIdAsync(cargosSetores.SetoresId);

            var resultado = await _cargosSetoresRepository.ExcluirCargosSetoresAsync(cargosSetores);

            return resultado;


           

           
        }
        catch (Exception)
        {

            throw;
        }
    }
    #endregion
}
