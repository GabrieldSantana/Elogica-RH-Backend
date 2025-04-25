using Dapper;
using Domain;
using Domain.Dtos;
using Domain.Models;
using Infrastructure.Interfaces;
using System.Data;

namespace Infrastructure.Repositories
{
    public class HorariosRepository : IHorariosRepository
    {
        private readonly IDbConnection _connection;

        public HorariosRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<bool> AdicionarHorarioAsync(HorariosDto horario)
        {
            try
            {
                var query = @"INSERT INTO Horarios (HorarioInicio, HorarioFim, IntervaloInicio, IntervaloFim)
                              VALUES (@HorarioInicio, @HorarioFim, @IntervaloInicio, @IntervaloFim)";

                var result = await _connection.ExecuteAsync(query, horario);
                return result > 0;
            }
            catch (Exception)
            {
                throw new Exception("Erro ao adicionar horário.");
            }
        }

        public async Task<bool> AtualizarHorarioAsync(int id, HorariosDto horario)
        {
            try
            {
                var query = @"UPDATE Horarios 
                              SET HorarioInicio = @HorarioInicio, 
                                  HorarioFim = @HorarioFim, 
                                  IntervaloInicio = @IntervaloInicio, 
                                  IntervaloFim = @IntervaloFim
                              WHERE Id = @Id";

                var parameters = new
                {
                    horario.HorarioInicio,
                    horario.HorarioFim,
                    horario.IntervaloInicio,
                    horario.IntervaloFim,
                    Id = id
                };

                var result = await _connection.ExecuteAsync(query, parameters);
                return result > 0;
            }
            catch (Exception)
            {
                throw new Exception("Erro ao atualizar horário.");
            }
        }

        public async Task<bool> ExcluirHorarioAsync(int id)
        {
            try
            {
                var query = @"DELETE FROM Horarios WHERE Id = @Id";

                var result = await _connection.ExecuteAsync(query, new { Id = id });
                return result > 0;
            }
            catch (Exception)
            {
                throw new Exception("Erro ao excluir horário.");
            }
        }

        public async Task<Horarios> BuscarHorarioPorIdAsync(int id)
        {
            try
            {
                var query = @"SELECT * FROM Horarios WHERE Id = @Id";

                var result = await _connection.QuerySingleOrDefaultAsync<Horarios>(query, new { Id = id });
                return result;
            }
            catch (Exception)
            {
                throw new Exception("Erro ao buscar horário por ID.");
            }
        }

        public async Task<List<Horarios>> BuscarTodosHorariosAsync()
        {
            try
            {
                var query = @"SELECT * FROM Horarios";

                var result = await _connection.QueryAsync<Horarios>(query);
                return result.ToList();
            }
            catch (Exception)
            {
                throw new Exception("Erro ao buscar todos os horários.");
            }
        }

        public async Task<RetornoPaginado<Horarios>> BuscarHorarioPaginadoAsync(int pagina, int quantidade)
        {
            try
            {
                var offset = (pagina - 1) * quantidade;

                var query = @"SELECT * FROM Horarios
                              ORDER BY Id
                              OFFSET @Offset ROWS FETCH NEXT @Quantidade ROWS ONLY;

                              SELECT COUNT(*) FROM Horarios;";

                using var multi = await _connection.QueryMultipleAsync(query, new { Offset = offset, Quantidade = quantidade });

                var horarios = (await multi.ReadAsync<Horarios>()).ToList();
                var total = await multi.ReadSingleAsync<int>();

                return new RetornoPaginado<Horarios>
                {
                    Registros = horarios,
                    TotalRegistro = total
                };
            }
            catch (Exception)
            {
                throw new Exception("Erro ao buscar horários paginados.");
            }
        }
    }
}
