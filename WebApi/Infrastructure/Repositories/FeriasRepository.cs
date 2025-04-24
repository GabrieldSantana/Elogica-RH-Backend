///*using Domain.Models;
//using Infrastructure.Interfaces;
//using AutoMapper;
//using Domain;


//namespace Infrastructure.Repositories
//{
//    public class FeriasRepository : IFeriasRepository
//    {
//        private readonly IMapper _mapper;
//        private static List<Ferias> _feriasData = new List<Ferias>(); 

//        public FeriasRepository(IMapper mapper)
//        {
//            _mapper = mapper;
//        }

//        public async Task<RetornoPaginado<Ferias>> GetPaginadoAsync(int pagina, int quantidade)
//        {
//            var query = _feriasData.AsQueryable();

//            var totalRegistro = query.Count();

//            var feriasEntities = query
//                .Skip((pagina - 1) * quantidade)
//                .Take(quantidade)
//                .ToList();

//            return new RetornoPaginado<Ferias>
//            {
//                TotalRegistro = totalRegistro,
//                Quantidade = quantidade,
//                Pagina = pagina,
//                RetornoPagina = _mapper.Map<List<Ferias>>(feriasEntities)
//            };
//        }

//        public async Task<IEnumerable<Ferias>> GetAllAsync()
//        {
//            return await Task.FromResult(_mapper.Map<IEnumerable<Ferias>>(_feriasData));
//        }
//        public async Task<IEnumerable<Ferias>> ObterFeriasDoFuncionarioNoAno(int funcionarioId, int ano)
//        {
//            return (await GetAllAsync())
//                .Where(f => f.FuncionariosId == funcionarioId &&
//                           f.DataInicio.Year == ano)
//                .ToList();
//        }
   
//        public async Task<Ferias> GetByIdAsync(int id)
//        {
//            var feriasEntity = _feriasData.FirstOrDefault(f => f.Id == id);
//            return await Task.FromResult(_mapper.Map<Ferias>(feriasEntity));
//        }

//        public async Task<Ferias> CreateAsync(Ferias ferias)
//        {
//            var feriasEntity = _mapper.Map<Ferias>(ferias);
//            feriasEntity.Id = _feriasData.Any() ? _feriasData.Max(f => f.Id) + 1 : 1;
//            _feriasData.Add(feriasEntity);
//            return await Task.FromResult(_mapper.Map<Ferias>(feriasEntity));
//        }

//        public async Task UpdateAsync(Ferias ferias)
//        {
//            var feriasEntity = _mapper.Map<Ferias>(ferias);
//            var existing = _feriasData.FirstOrDefault(f => f.Id == feriasEntity.Id);
//            if (existing != null)
//            {
//                _feriasData.Remove(existing);
//                _feriasData.Add(feriasEntity);
//            }
//            await Task.CompletedTask;
//        }

//        public async Task DeleteAsync(int id)
//        {
//            var feriasEntity = _feriasData.FirstOrDefault(f => f.Id == id);
//            if (feriasEntity != null)
//            {
//                _feriasData.Remove(feriasEntity);
//            }
//            await Task.CompletedTask;
//        }

//        public async Task<bool> FuncionarioTemFerias(int funcionarioId, DateTime dataInicio, DateTime dataFim)
//        {
//            return await Task.FromResult(_feriasData.Any(f =>
//                f.FuncionariosId == funcionarioId &&
//                ((dataInicio >= f.DataInicio && dataInicio <= f.DataFim) ||
//                 (dataFim >= f.DataInicio && dataFim <= f.DataFim) ||
//                 (dataInicio <= f.DataInicio && dataFim >= f.DataFim))));
//        }
//    }
//}*/


//using Dapper;
//using Domain;
//using Domain.Models;
//using Infrastructure.Interfaces;
//using Microsoft.Data.SqlClient;
//using Microsoft.Extensions.Configuration;


//namespace Infrastructure.Repositories
//{
//    public class FeriasRepository : IFeriasRepository
//    {
//        private readonly string _connectionString;

//        public FeriasRepository(IConfiguration configuration)
//        {
//            _connectionString = configuration.GetConnectionString("DefaultConnection");
//        }

//        public async Task<RetornoPaginado<Ferias>> GetPaginadoAsync(int pagina, int tamanhoPagina)
//        {
//            using (var connection = new SqlConnection(_connectionString))
//            {
//                await connection.OpenAsync();

//                var offset = (pagina - 1) * tamanhoPagina;

                
//                var queryItens = @"
//                    SELECT 
//                        f.Id, f.DataInicio, f.DataFim, f.FuncionarioId,
//                        fu.Id, fu.Nome, fu.CPF, fu.DataNascimento, fu.Email, 
//                        fu.Telefone, fu.Endereco, fu.DataContratacao, fu.Salario,
//                        fu.Ativo, fu.CargosId, fu.SetoresId, fu.HorariosId
//                    FROM Ferias f
//                    INNER JOIN Funcionarios fu ON f.FuncionarioId = fu.Id
//                    ORDER BY f.Id
//                    OFFSET @Offset ROWS
//                    FETCH NEXT @TamanhoPagina ROWS ONLY";

//                var queryTotal = "SELECT COUNT(*) FROM Ferias";

            
//                var queryItensTask = connection.QueryAsync<Ferias, Funcionario, Ferias>(
//                    queryItens,
//                    (ferias, funcionario) =>
//                    {
//                        ferias.Funcionario = funcionario;
//                        return ferias;
//                    },
//                    new { Offset = offset, TamanhoPagina = tamanhoPagina },
//                    splitOn: "Id"
//                );

//                var queryTotalTask = connection.ExecuteScalarAsync<int>(queryTotal);

//                await Task.WhenAll(queryItensTask, queryTotalTask);

//                var itens = (await queryItensTask).ToList();
//                var totalRegistros = await queryTotalTask;

//                return new RetornoPaginado<Ferias>
//                {
//                    RetornoPagina = itens,
//                    TotalRegistro = totalRegistros,
//                    Quantidade = itens.Count,
//                    Pagina = pagina
//                };
//            }
//        }

//        public async Task<IEnumerable<Ferias>> BuscarFeriasAsync()
//        {
//            using (var connection = new SqlConnection(_connectionString))
//            {
//                await connection.OpenAsync();

//                var query = @"
//            SELECT 
//                f.Id, f.DataInicio, f.DataFim, f.FuncionarioId,
//                fu.Id, fu.Nome, fu.CPF, fu.DataNascimento, fu.Email, 
//                fu.Telefone, fu.Endereco, fu.DataContratacao, fu.Salario,
//                fu.Ativo, fu.CargosId, fu.SetoresId, fu.HorariosId
//            FROM Ferias f
//            INNER JOIN Funcionarios fu ON f.FuncionarioId = fu.Id";

//                var feriasDict = new Dictionary<int, Ferias>();

//                await connection.QueryAsync<Ferias, Funcionario, Ferias>(
//                    query,
//                    (ferias, funcionario) =>
//                    {
//                        if (!feriasDict.TryGetValue(ferias.Id, out var feriasEntry))
//                        {
//                            feriasEntry = ferias;
//                            feriasEntry.Funcionario = funcionario;
//                            feriasDict.Add(feriasEntry.Id, feriasEntry);
//                        }
//                        return feriasEntry;
//                    },
//                    splitOn: "Id"
//                );

//                return feriasDict.Values;
//            }
//        }

//        public async Task<Ferias> BuscarFeriasPorIdAsync(int id)
//        {
//            using (var connection = new SqlConnection(_connectionString))
//            {
//                await connection.OpenAsync();

//                var query = @"
//                    SELECT 
//                        f.Id, f.DataInicio, f.DataFim, f.FuncionarioId,
//                        fu.Id, fu.Nome, fu.CPF, fu.DataNascimento, fu.Email, 
//                        fu.Telefone, fu.Endereco, fu.DataContratacao, fu.Salario,
//                        fu.Ativo, fu.CargosId, fu.SetoresId, fu.HorariosId
//                    FROM Ferias f
//                    INNER JOIN Funcionarios fu ON f.FuncionarioId = fu.Id
//                    WHERE f.Id = @Id";

//                var result = await connection.QueryAsync<Ferias, Funcionario, Ferias>(
//                    query,
//                    (ferias, funcionario) =>
//                    {
//                        ferias.Funcionario = funcionario;
//                        return ferias;
//                    },
//                    new { Id = id },
//                    splitOn: "Id"
//                );

//                return result.FirstOrDefault();
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
//                            Id = ferias.Id,
//                            DataInicio = ferias.DataInicio,
//                            DataFim = ferias.DataFim,
//                            FuncionarioId = ferias.FuncionarioId
//                        }, transaction);

//                        transaction.Commit();
//                    }
//                    catch
//                    {
//                        transaction.Rollback();
//                        throw;
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