using System.Data;
using Dapper;
using Domain.Models;
using Infrastructure.Interfaces;

namespace Infrastructure.Repositories
{
    class CargosRepository : ICargosRepository
    {
        private readonly IDbConnection _conn;

        public CargosRepository(IDbConnection conn)
        {
            _conn = conn;
        }

        public async Task<int> AdicionarCargosAsync(Cargos cargos)
        {
            try
            {
                string sql = @"
                INSERT INTO Cargos (Titulo, Descricao, SalarioBase)
                OUTPUT INSERTED.Id
                VALUES (@Titulo, @Descricao, @SalarioBase)";

                var parametros = new
                {
                    Titulo = cargos.Titulo.Trim(),
                    Descricao = cargos.Descricao,
                    SalarioBase = cargos.SalarioBase
                };

                return await _conn.ExecuteScalarAsync<int>(sql, parametros);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> AtualizarCargosAsync(Cargos cargos)
        {
            try
            {
                string sql = @"UPDATE Cargos 
                             SET Titulo = @Titulo, 
                                 Descricao = @Descricao,
                                 SalarioBase = @SalarioBase
                             WHERE Id = @Id";

                var parametros = new
                {
                    Id = cargos.Id,
                    Titulo = cargos.Titulo.Trim(),
                    Descricao = cargos.Descricao,
                    SalarioBase = cargos.SalarioBase
                };

                var linhasAfetadas = await _conn.ExecuteAsync(sql, parametros);
                return linhasAfetadas > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Cargos>> BuscarCargosAsync()
        {
            try
            {
                string sql = @"SELECT * FROM Cargos ORDER BY Id";

                return await _conn.QueryAsync<Cargos>(sql);

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<RetornoPaginado<Cargos>> BuscarCargosPaginadoAsync(int pagina, int quantidade)
        {
            try
            {
                string sql = @"SELECT Id, Titulo, Descricao, SalarioBase
                            FROM Cargos 
                            ORDER BY Id 
                            OFFSET @Offset ROWS FETCH NEXT @Quantidade ROWS ONLY";

                var parametros = new
                {
                    Offset = (pagina - 1) * quantidade,
                    Quantidade = quantidade
                };

                var cargos = (await _conn.QueryAsync<Cargos>(sql, parametros)).ToList();

                var totalCargos = await _conn.ExecuteScalarAsync<int>("SELECT COUNT(*) FROM Cargos");

                return new RetornoPaginado<Cargos>
                {
                    TotalRegistro = totalCargos,
                    Registros = cargos
                };
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Cargos> BuscarCargosPorIdAsync(int id)
        {
            try
            {
                string sql = "SELECT * FROM Cargos WHERE Id = @Id";
                return await _conn.QueryFirstOrDefaultAsync<Cargos>(sql, new { Id = id });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> ExcluirCargosAsync(int id)
        {
            try
            {
                string sql = "DELETE FROM Cargos WHERE Id = @Id";
                var linhasAfetadas = await _conn.ExecuteAsync(sql, new { Id = id });
                return linhasAfetadas > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
