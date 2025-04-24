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
        throw new NotImplementedException();
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

  
}
