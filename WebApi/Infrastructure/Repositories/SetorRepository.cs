using System.Data;
using Dapper;
using Domain;
using Domain.Models;
using Infrastructure.Interfaces;

namespace Infrastructure.Repositories;

internal class SetorRepository : ISetorRepository
{
    private readonly IDbConnection _connection;
    public SetorRepository(IDbConnection connection)
    {
        _connection = connection;
    }
    public async Task<Setor> AdicionarSetoresAsync(Setor setor)
    {
        throw new NotImplementedException();
    }

    public async Task<Setor> AtualizarSetoresAsync(Setor setor)
    {
        throw new NotImplementedException();
    }

    public async Task<Setor> BuscarSetoresPorIdAsync(int id)
    {
        try
        {
            string sql = $"SELECT * FROM Setores WHERE Id={id}";
            var setor = await _connection.QueryFirstOrDefaultAsync<Setor>(sql);
            if (setor == null)
            {
                throw new Exception("Setor não encontrado");
            }
            return setor;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<RetornoPaginado<Setor>> BuscarSetoresPaginadoAsync(int pagina, int quantidade)
    {
        try
        {
            string sql = "SELECT * FROM SETORES ORDER BY ID OFFSET @OFFSET ROWS FETCH NEXT @QUANTIDADE ROWS ONLY";

            var parametros = new
            {
                OFFSET = (pagina - 1) * quantidade,
                QUANTIDADE = quantidade
            };

            var setores = await _connection.QueryAsync<Setor>(sql, parametros);

            var totalSetores = "SELECT COUNT(*) FROM Setores";

            var retornoTotalSetores = await _connection.ExecuteScalarAsync<int>(totalSetores);

            var retornoPaginado = new RetornoPaginado<Setor>
            {
                TotalRegistro = retornoTotalSetores,
                Registros = setores.ToList()
            };

            return retornoPaginado;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<IEnumerable<Setor>> BuscarTodosSetoresAsync()
    {
        try
        {
            string sql = "SELECT * FROM Setores";
            var setores = await _connection.QueryAsync<Setor>(sql);
            return setores;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<bool> ExcluirSetoresAsync(int id)
    {
        try
        {
            string sql = "DELETE FROM Setores WHERE Id = @Id";
            var parameters = new { Id = id };
            var result = await _connection.ExecuteAsync(sql, parameters);
            return result > 0;
        }
        catch (Exception)
        {
            throw;
        };
    }
}
