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

    public async Task<int> AdicionarCargosSetoresAsync(CargosSetores cargosSetores)
    {
        try
        {
            var sqlInsert = @"insert into CargosSetores(CargosId, SetoresId) values(@cargoId, @SETORESID);";

            var paramentros = new
            {
                CARGOSID = cargosSetores.CargosId,
                SETORESID = cargosSetores.SetoresId
            };

            var resultado = await _conn.ExecuteAsync(sqlInsert, paramentros);

            return resultado;

        }
        catch (Exception)
        {

            throw;
        }
    }

    public Task<bool> AtualizarCargosSetoresAsync(CargosSetores cargosSetores)
    {
        try
        {
            var sqlUpdate = @"update CargosSetores set CargosId = @cargosid2,
            SetoresId = @Setoresid 
            where CargosId = @cargoID 
            and SetoresId = @SetoresId";

            var parametros = new { 
            

            };



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
            var sqlSelect = @"SELECT * FROM CARGOSSETORES";

            var resultado = await _conn.QueryAsync<CargosSetores>(sqlSelect);
            return resultado.ToList();
        }
        catch (Exception)
        {

            throw;
        }
    }

    public async Task<bool> ExcluirCargosSetoresAsync(CargosSetores cargosSetores)
    {
        try
        {
            var sqlDelete = @"delete from CargosSetores where CargosId = @cargosId and SetoresId = @setoresID";


            var parametros = new
            {
                CARGOSID = cargosSetores.CargosId,
                SETORESID = cargosSetores.SetoresId
            };

            var retorno = await _conn.ExecuteAsync(sqlDelete, parametros);

            return retorno > 0;
        }
        catch (Exception)
        {

            throw;
        }
    }
}
