using System.Data;
using Dapper;
using Domain.Models;
using Infrastructure.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;


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

        public async Task<IEnumerable<Ferias>> BuscarFeriasAsync()
        {
            var sql = @"
            SELECT 
                f.Id, f.DataInicio, f.DataFim, f.FuncionarioId
            FROM Ferias f";

            var result = await _connection.QueryAsync<Ferias>(sql);
            return result;
        }

        public async Task<Ferias> BuscarFeriasPorIdAsync(int id)
        {
            var sql = @"
            SELECT 
                f.Id, f.DataInicio, f.DataFim, f.FuncionarioId
            FROM Ferias f
            WHERE f.Id = @Id";

            var result = await _connection.QueryFirstOrDefaultAsync<Ferias>(sql, new { Id = id });
            return result;
        }

        public async Task<Ferias> AdicionarFeriasAsync(Ferias ferias)
        {
            using (var transaction = _connection.BeginTransaction())
            {
                try
                {
                    //Verifica se o funcionário existe
                    var funcionarioExists = await _connection.ExecuteScalarAsync<bool>(
                        "SELECT 1 FROM Funcionarios WHERE Id = @Id AND Ativo = 1",
                        new { Id = ferias.FuncionarioId },
                        transaction
                    );

                    if (!funcionarioExists)
                        throw new Exception("Funcionário não encontrado ou inativo");

                    //Valida período mínimo de 1 ano de contratação
                    var dataContratacao = await _connection.ExecuteScalarAsync<DateTime>(
                        "SELECT DataContratacao FROM Funcionarios WHERE Id = @Id",
                        new { Id = ferias.FuncionarioId },
                        transaction
                    );

                    if (dataContratacao.AddYears(1) > DateTime.Now)
                        throw new Exception("Funcionário precisa ter pelo menos 1 ano de empresa");

                    //Insere as férias
                    var query = @"
                    INSERT INTO Ferias (DataInicio, DataFim, FuncionarioId)
                    OUTPUT INSERTED.Id
                    VALUES (@DataInicio, @DataFim, @FuncionarioId)";

                    var id = await _connection.ExecuteScalarAsync<int>(query, new
                    {
                        DataInicio = ferias.DataInicio,
                        DataFim = ferias.DataFim,
                        FuncionarioId = ferias.FuncionarioId
                    }, transaction);

                    transaction.Commit();
                    ferias.Id = id;
                    return ferias;
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public async Task AtualizarFeriasAsync(Ferias ferias)
        {
            using (var transaction = _connection.BeginTransaction())
            {
                try
                {
                    //Verifica se a data final é maior que a inicial
                    if (ferias.DataFim <= ferias.DataInicio)
                        throw new Exception("Data final deve ser maior que data inicial");

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
                    }, transaction);

                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw new Exception("Não foi possível atualizar o ID informado");
                }
            }
        }

        public async Task ExcluirFeriasAsync(int id)
        {
            await _connection.ExecuteAsync(
                "DELETE FROM Ferias WHERE Id = @Id",
                new { Id = id });
        }

        public async Task<bool> FuncionarioTemFerias(int funcionarioId, DateTime dataInicio, DateTime dataFim)
        {
            var query = @"
            SELECT COUNT(*) 
            FROM Ferias 
            WHERE FuncionarioId = @FuncionarioId
            AND (
                (@DataInicio BETWEEN DataInicio AND DataFim) OR
                (@DataFim BETWEEN DataInicio AND DataFim) OR
                (DataInicio BETWEEN @DataInicio AND @DataFim)
            )";

            var count = await _connection.ExecuteScalarAsync<int>(query, new
            {
                FuncionarioId = funcionarioId,
                DataInicio = dataInicio,
                DataFim = dataFim
            });

            return count > 0;
        }
    }

}
//    public class FeriasRepository : IFeriasRepository
//    {
//        private readonly string _connectionString;

//        public FeriasRepository(IConfiguration configuration)
//        {
//            _connectionString = configuration.GetConnectionString("DefaultConnection");
//        }

//        public async Task<RetornoPaginado<Ferias>> BuscarFeriasPaginadoAsync(int pagina, int quantidade)
//        {
//            using (var connection = new SqlConnection(_connectionString))
//            {
//                await connection.OpenAsync();

//                var offset = (pagina - 1) * quantidade;

//                var sql = @"
//                    SELECT 
//                        f.Id, f.DataInicio, f.DataFim, f.FuncionarioId,
//                        fu.Id
//                    FROM Ferias f
//                    INNER JOIN Funcionarios fu ON f.FuncionarioId = fu.Id
//                    ORDER BY f.Id
//                    OFFSET @Offset ROWS FETCH NEXT @Quantidade ROWS ONLY";

//                var paramentros = new
//                {
//                    Offset = pagina,
//                    Quantidade = quantidade
//                };
//                var totalQuery = "SELECT COUNT(*) FROM Ferias";

//                var registros = await connection.QueryAsync<Ferias>(sql, paramentros);
                   

//                var totalRegistro = await connection.ExecuteScalarAsync<int>(totalQuery);

//                return new RetornoPaginado<Ferias>
//                {
//                    TotalRegistro = totalRegistro,
//                    Registros = registros.ToList()
//                }; 
//            }
//        }

//        public async Task<IEnumerable<Ferias>> BuscarFeriasAsync()
//        {
//            //using (var connection = new SqlConnection(_connectionString))
            
//               // await connection.OpenAsync();

//                var sql = @"
//            SELECT 
//                f.Id, f.DataInicio, f.DataFim, f.FuncionarioId,
//                fu.Id
//            FROM Ferias f
//            INNER JOIN Funcionarios fu ON f.FuncionarioId = fu.Id";

//                var result = await connection.QueryAsync<Ferias>(sql);
//                return result;
//            }
        

//        public async Task<Ferias> BuscarFeriasPorIdAsync(int id)
//        {
//            using (var connection = new SqlConnection(_connectionString))
//            {
//                await connection.OpenAsync();

//                var sql = $@"
//                    SELECT 
//                        f.Id, f.DataInicio, f.DataFim, f.FuncionarioId,
//                        fu.Id
//                    FROM Ferias f
//                    INNER JOIN Funcionarios fu ON f.FuncionarioId = fu.Id
//                    WHERE f.Id = {id}";

//                var result = await connection.QueryFirstOrDefaultAsync<Ferias>(sql);
//                return result;

//            }
//        }

//        public async Task<Ferias> AdicionarFeriasAsync(Ferias ferias)
//        {
//            using (var connection = new SqlConnection(_connectionString))
//            {
//                await connection.OpenAsync();

//                using (var transaction = connection.BeginTransaction())
//                {
//                    try
//                    {
//                        //Verifica se o funcionário existe
//                        var funcionarioExists = await connection.ExecuteScalarAsync<bool>(
//                            "SELECT 1 FROM Funcionarios WHERE Id = @Id AND Ativo = 1",
//                            new { Id = ferias.FuncionarioId },
//                            transaction
//                        );

//                        if (!funcionarioExists)
//                            throw new Exception("Funcionário não encontrado ou inativo");

//                        //Valida período mínimo de 1 ano de contratação
//                        var dataContratacao = await connection.ExecuteScalarAsync<DateTime>(
//                            "SELECT DataContratacao FROM Funcionarios WHERE Id = @Id",
//                            new { Id = ferias.FuncionarioId },
//                            transaction
//                        );

//                        if (dataContratacao.AddYears(1) > DateTime.Now)
//                            throw new Exception("Funcionário precisa ter pelo menos 1 ano de empresa");

//                        //Insere as férias
//                        var query = @"
//                            INSERT INTO Ferias (DataInicio, DataFim, FuncionarioId)
//                            OUTPUT INSERTED.Id
//                            VALUES (@DataInicio, @DataFim, @FuncionarioId)";

//                        var id = await connection.ExecuteScalarAsync<int>(query, new
//                        {
//                            DataInicio = ferias.DataInicio,
//                            DataFim = ferias.DataFim,
//                            FuncionarioId = ferias.FuncionarioId
//                        }, transaction);

//                        transaction.Commit();
//                        ferias.Id = id;
//                        return ferias;
//                    }
//                    catch
//                    {
//                        transaction.Rollback();
//                        throw;
//                    }
//                }
//            }
//        }

//        public async Task AtualizarFeriasAsync(Ferias ferias)
//        {
//            using (var connection = new SqlConnection(_connectionString))
//            {
//                await connection.OpenAsync();

//                using (var transaction = connection.BeginTransaction())
//                {
//                    try
//                    {
//                        //Verifica se a data final é maior que a inicial
//                        if (ferias.DataFim <= ferias.DataInicio)
//                            throw new Exception("Data final deve ser maior que data inicial");

//                        var query = @"
//                            UPDATE Ferias 
//                            SET 
//                                DataInicio = @DataInicio, 
//                                DataFim = @DataFim, 
//                                FuncionarioId = @FuncionarioId
//                            WHERE Id = @Id";

//                        await connection.ExecuteAsync(query, new
//                        {
//                           // Id = ferias.Id,
//                            DataInicio = ferias.DataInicio,
//                            DataFim = ferias.DataFim,
//                            FuncionarioId = ferias.FuncionarioId
//                        });
//                    }
//                    catch(Exception)
//                    {
//                        throw new Exception ("Não foi possível atualizar o ID informado");
//                    }
//                }
//            }
//        }

//        public async Task ExcluirFeriasAsync(int id)
//        {
//            using (var connection = new SqlConnection(_connectionString))
//            {
//                await connection.OpenAsync();
//                await connection.ExecuteAsync(
//                    "DELETE FROM Ferias WHERE Id = @Id",
//                    new { Id = id });
//            }
//        }

//        public async Task<bool> FuncionarioTemFerias(int funcionarioId, DateTime dataInicio, DateTime dataFim)
//        {
//            using (var connection = new SqlConnection(_connectionString))
//            {
//                await connection.OpenAsync();

//                var query = @"
//                    SELECT COUNT(*) 
//                    FROM Ferias 
//                    WHERE FuncionarioId = @FuncionarioId
//                    AND (
//                        (@DataInicio BETWEEN DataInicio AND DataFim) OR
//                        (@DataFim BETWEEN DataInicio AND DataFim) OR
//                        (DataInicio BETWEEN @DataInicio AND @DataFim)
//                    )";

//                var count = await connection.ExecuteScalarAsync<int>(query, new
//                {
//                    FuncionarioId = funcionarioId,
//                    DataInicio = dataInicio,
//                    DataFim = dataFim
//                });

//                return count > 0;
//            }
//        }
//    }
//}