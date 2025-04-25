using Application.Interfaces;
using Domain.Models;
using Infrastructure.Interfaces;

namespace Application.Services;

public class CargosSetoresService : ICargosSetoresService
{

    private readonly ICargosSetoresRepository _cargosSetoresRepository;

    public CargosSetoresService(ICargosSetoresRepository cargosSetoresRepository)
    {
        _cargosSetoresRepository = cargosSetoresRepository;
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

            if(cargosSetoresAntigo.SetoresId == cargosSetoresNovo.SetoresId && cargosSetoresNovo.SetoresId == cargosSetoresNovo.CargosId)
            {
                throw new Exception("Os valores do novo relacionamento são iguais aos do relacionamento antigo. Nenhuma atualização é necessária.");

            }

            

            var atualizarCargosSetores = await _cargosSetoresRepository.AtualizarCargosSetoresAsync(cargosSetoresAntigo, cargosSetoresNovo);

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
    public async Task<bool> ExcluirCargosSetoresAsync(int cargosId)
    {
        try
        {
           if(cargosId <= 0)
            {
                throw new ArgumentException("O Id fornecido deve ser positivo e maior que zero");
            }

            var cargosIdExistente = await _cargosSetoresRepository.VerificarCargosSetoresAsync(cargosId);

            if (!cargosIdExistente)
            {
                throw new ArgumentException("O CargosId fornecido não existe no banco de dados");
            }


            var excluirCargosSetores = await _cargosSetoresRepository.ExcluirCargosSetoresAsync(cargosId);

            return excluirCargosSetores;
        }
        catch (Exception)
        {

            throw;
        }
    }
    #endregion
}
