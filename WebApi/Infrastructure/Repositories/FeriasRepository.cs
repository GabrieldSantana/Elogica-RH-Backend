using System.Data;
using Dapper;
using Domain.Dtos;
using Domain.Models;
using Infrastructure.Interfaces;


namespace Infrastructure.Repositories
{
    public class FeriasRepository : IFeriasRepository
    {
        private readonly IDbConnection _connection;

        public FeriasRepository(IDbConnection connection)
        {
            _connection = connection;
        }
        public async Task<RetornoPaginado<Ferias>> BuscarFeriasPaginadoAsync(int pagina, int quantidade)
        {
            try
            {
                var offset = (pagina - 1) * quantidade;

                var sql = @"
            SELECT 
                f.Id, f.DataInicio, f.DataFim, f.FuncionarioId
            FROM Ferias f
            ORDER BY f.Id
            OFFSET @Offset ROWS FETCH NEXT @Quantidade ROWS ONLY";

                var paramentros = new
                {
                    Offset = pagina,
                    Quantidade = quantidade
                };
                var totalQuery = "SELECT COUNT(*) FROM Ferias";

                var registros = await _connection.QueryAsync<Ferias>(sql, paramentros);

                var totalRegistro = await _connection.ExecuteScalarAsync<int>(totalQuery);

                return new RetornoPaginado<Ferias>
                {
                    TotalRegistro = totalRegistro,
                    Registros = registros.ToList()
                };
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<IEnumerable<Ferias>> BuscarFeriasAsync()
        {
            try
            {
                var sql = "SELECT * FROM Ferias";
                return await _connection.QueryAsync<Ferias>(sql);
            }
            catch (Exception)
            {
                throw;
            }

        }
        public async Task<Ferias> BuscarFeriasPorIdAsync(int id)
        {
            try
            {
                var sql = @"
            SELECT 
                f.Id, f.DataInicio, f.DataFim, f.FuncionarioId
            FROM Ferias f
            WHERE f.Id = @Id";

                var retorno = await _connection.QueryFirstOrDefaultAsync<Ferias>(sql, new { Id = id });
                return retorno;
            }
            catch (Exception)
            {
                throw;
            }
        }

   

        public async Task AtualizarFeriasAsync(Ferias ferias)
        {
            try
            {
                var query = @"
            UPDATE Ferias 
            SET 
                DataInicio = @DataInicio, 
                DataFim = @DataFim, 
                FuncionarioId = @FuncionarioId
            WHERE Id = @Id";

                await _connection.ExecuteAsync(query, new
                {
                    Id = ferias.Id,
                    DataInicio = ferias.DataInicio,
                    DataFim = ferias.DataFim,
                    FuncionarioId = ferias.FuncionarioId
                });
            }
            catch
            {
                throw;
            }

        }
        public async Task ExcluirFeriasAsync(int id)
        {
            try
            {
                await _connection.ExecuteAsync(
                    "DELETE FROM Ferias WHERE Id = @Id",
                    new { Id = id });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> AdicionarFeriasAsync(FeriasDto ferias)
        {
            try
            {
                //Insere as férias
                var query = @"
                    INSERT INTO Ferias (DataInicio, DataFim, FuncionarioId)
                    VALUES (@DataInicio, @DataFim, @FuncionarioId)";

                var feriasFuncionario = await _connection.ExecuteAsync(query, new
                {
                    DataInicio = ferias.DataInicio,
                    DataFim = ferias.DataFim,
                    FuncionarioId = ferias.FuncionarioId
                });
                return feriasFuncionario > 0 ? true : false;
            }
            catch
            {
                throw;
            }
        }
    }
}


