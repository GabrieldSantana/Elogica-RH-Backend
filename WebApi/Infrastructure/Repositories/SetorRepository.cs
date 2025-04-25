using System.Data;
using Dapper;
using Domain.Models;
using Infrastructure.Interfaces;

namespace Infrastructure.Repositories;

public class SetorRepository : ISetorRepository
{
    private readonly IDbConnection _connection;
    public SetorRepository(IDbConnection connection)
    {
        _connection = connection;
    }

    public async Task<bool> AdicionarSetoresAsync(Setor setor)
    {
        try
        {
            string sql = "INSERT INTO SETORES (Nome, Descricao) VALUES (@Nome, @Descricao)";

            var parametros = new
            {
                setor.Nome,
                setor.Descricao
            };

            var setoresCadastrados = await _connection.ExecuteAsync(sql, parametros);
            return setoresCadastrados > 0 ? true : false;
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public async Task<bool> AtualizarSetoresAsync(Setor setor)
    {
        try
        {
            string sql = @"UPDATE SETORES SET Nome = @Nome, Descricao = @Descricao WHERE Id = @Id";
            var parametros = new
            {
                Nome = setor.Nome,
                Descricao = setor.Descricao,
                Id = setor.Id,
            };

            var resultado = await _connection.ExecuteAsync(sql, parametros);
            return resultado > 0 ? true : false;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<Setor> BuscarSetoresPorIdAsync(int id)
    {
        try
        {
            string sql = $"SELECT * FROM Setores WHERE Id={id}";
            var setor = await _connection.QueryFirstOrDefaultAsync<Setor>(sql);
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

    public async Task<IEnumerable<Setor>> BuscarSetoresAsync()
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
