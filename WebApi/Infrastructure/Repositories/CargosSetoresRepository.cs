using System.Data;
using Dapper;
using Domain.Models;
using Infrastructure.Interfaces;

namespace Infrastructure.Repositories;

public class CargosSetoresRepository : ICargosSetoresRepository
{

    private readonly IDbConnection _conn;

    public CargosSetoresRepository(IDbConnection conn)
    {
        _conn = conn;
    }

    #region Adicionar CargosSetores
    public async Task<int> AdicionarCargosSetoresAsync(CargosSetores cargosSetores)
    {
        try
        {
            var sqlInsert = @"insert into CargosSetores(CargosId, SetoresId) values(@cargosId, @SETORESID)";

            var paramentros = new
            {
                CARGOSID = cargosSetores.CargosId,
                SETORESID = cargosSetores.SetoresId
            };

            var resultado = await _conn.ExecuteAsync(sqlInsert, paramentros);

            return resultado;

        }
        catch (Exception ex)
        {

            throw new Exception("erro ao adicionar CargosSetores");
        }
    }
    #endregion

    #region Atualizar CargosSetores
    //Voltar mais tarde 
    public Task<bool> AtualizarCargosSetoresAsync(CargosSetores cargosSetores)
    {
        throw new NotImplementedException();
    }
    #endregion

    #region Buscar cargosSetores
    public async Task<List<CargosSetores>> BuscarCargosSetoresAsync()
    {
        try
        {
            var sqlSelect = @"SELECT * FROM CARGOSSETORES";

            var resultado = await _conn.QueryAsync<CargosSetores>(sqlSelect);
            return resultado.ToList();
        }
        catch (Exception)
        {

            throw new Exception("Erro ao buscar CargosSetores");
        }
    }
    #endregion

    #region Buscar CargosSetores paginados
    public async Task<RetornoPaginado<CargosSetores>> BuscarCargosSetoresPaginadoAsync(int quantidade, int pagina)
    {
        try
        {
            string sql = "SELECT * FROM CARGOSSETORES ORDER BY CARGOSID OFFSET @OFFSET ROWS FETCH NEXT @QUANTIDADE ROWS ONLY";

            var parametros = new
            {
                OFFSET = (pagina - 1) * quantidade,
                QUANTIDADE = quantidade
            };

            var cargosSetores = await _conn.QueryAsync<CargosSetores>(sql, parametros);
            var totalCargosSetores = "SELECT COUNT(*) FROM CARGOSSETORES";

            var retornoTotalCargosSetores = await _conn.ExecuteScalarAsync<int>(totalCargosSetores);

            var retornoPaginado = new RetornoPaginado<CargosSetores>
            {
                TotalRegistro = retornoTotalCargosSetores,
                Registros = cargosSetores.ToList()
            };

            return retornoPaginado;

        }
        catch (Exception ex)
        {
            throw new Exception("Erro ao realizar busca paginada de CargosSetores");
        }
    }

    #endregion

    #region Excluir CargosSetores 
    public async Task<bool> ExcluirCargosSetoresAsync(int id)
    {
        try
        {
            var sqlDelete = string.Format("delete from CargosSetores where CargosId ={0} ", id);




            var retorno = await _conn.ExecuteAsync(sqlDelete);

            return retorno > 0;
        }
        catch (Exception)
        {

            throw new Exception("Erro ao deleter cargosSetores");
        }
    }
    #endregion
}
