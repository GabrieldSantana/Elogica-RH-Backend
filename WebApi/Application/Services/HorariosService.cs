using Application.Interfaces;
using Domain.Dtos;
using Domain.Models;
using Infrastructure.Interfaces;

namespace Application.Services
{
    public class HorariosService : IHorariosService
    {
        private readonly IHorariosRepository _repository;

        public HorariosService(IHorariosRepository repository)
        {
            _repository = repository;
        }

        #region Método para impedir adição de horários iguais
        private async Task<bool> HorarioConflitanteExiste(HorariosDto horario, int? idExistente = null)
        {
            var todosHorarios = await _repository.BuscarHorariosAsync();

            return todosHorarios.Any(h =>
                h.Id != idExistente &&
                h.HorarioInicio == horario.HorarioInicio &&
                h.HorarioFim == horario.HorarioFim &&
                h.IntervaloInicio == horario.IntervaloInicio &&
                h.IntervaloFim == horario.IntervaloFim);
        }
        #endregion

        #region Atualizar Horário
        public async Task<string> AtualizarHorarioAsync(int id, HorariosDto horario)
        {
            string retorno = "";
            try
            {
                var horarioExistente = await _repository.BuscarHorarioPorIdAsync(id);
                if (horario == null)
                    throw new ArgumentException("Os dados do horário não podem ser nulos.");

                if (await HorarioConflitanteExiste(horario, id))
                    throw new ArgumentException("Já existe outro horário com esses mesmos valores.");

                if (horario.HorarioInicio.Hour < 8 || horario.HorarioFim.Hour > 20)
                    throw new ArgumentException("Os horários devem estar entre 08:00 e 20:00.");

                if (horario.HorarioInicio.Hour > 10)
                    throw new ArgumentException("O horário de início não pode ser após as 10:00.");

                if (horario.IntervaloInicio.Hour < 12 || horario.IntervaloFim.Hour > 14)
                    throw new ArgumentException("O intervalo só pode acontecer entre 12:00 e 14:00.");

                var duracaoIntervalo = horario.IntervaloFim - horario.IntervaloInicio;
                if (duracaoIntervalo.TotalHours != 1 && duracaoIntervalo.TotalHours != 2)
                    throw new ArgumentException("O intervalo só pode ter duração de 1h ou 2h.");

                var todosHorarios = new[] {
                horario.HorarioInicio, horario.HorarioFim,
                horario.IntervaloInicio, horario.IntervaloFim
                };

                if (todosHorarios.Any(h => h.Minute != 0 || h.Second != 0))
                    throw new ArgumentException("Todos os horários precisam começar e finalizar em horas fechadas (exemplo: 08:00).");

                if (horario.HorarioInicio >= horario.HorarioFim)
                    throw new ArgumentException("O horário de início precisa ser anterior ao horário de fim.");

                if (horario.IntervaloInicio >= horario.IntervaloFim)
                    throw new ArgumentException("O início do intervalo precisa ser anterior ao fim.");

                bool sucesso = await _repository.AtualizarHorarioAsync(id, horario);

                if (sucesso)
                {
                    retorno = "Horário atualizado com sucesso!";
                }

                return retorno;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region Buscar Horário Paginado
        public async Task<RetornoPaginado<Horarios>> BuscarHorarioPaginadoAsync(int pagina, int quantidade)
        {
            try
            {
                if (pagina < 1 || quantidade < 1)
                    throw new ArgumentException("Os parâmetros de paginação devem ser maiores que zero.");

                return await _repository.BuscarHorarioPaginadoAsync(pagina, quantidade);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region Buscar Horário por Id
        public async Task<Horarios> BuscarHorarioPorIdAsync(int id)
        {
            try
            {
                var horario = await _repository.BuscarHorarioPorIdAsync(id);
                if (horario == null)
                    throw new ArgumentException("Horário não encontrado.");

                return horario;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region Buscar Horários
        public async Task<List<Horarios>> BuscarHorariosAsync()
        {
            try
            {
                return await _repository.BuscarHorariosAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region AdicionarHorario
        public async Task<string> AdicionarHorarioAsync(HorariosDto horario)
        {
            string retorno = "";
            try
            {
                if (horario == null)
                    throw new ArgumentException("Os dados do horário não podem ser nulos.");

                if (await HorarioConflitanteExiste(horario))
                    throw new ArgumentException("Já existe um horário com esses mesmos valores.");

                if (horario.HorarioInicio.Hour < 8 || horario.HorarioFim.Hour > 20)
                    throw new ArgumentException("Os horários devem estar entre 08:00 e 20:00.");

                if (horario.HorarioInicio.Hour > 10)
                    throw new ArgumentException("O horário de início não pode ser após as 10:00.");

                if (horario.IntervaloInicio.Hour < 12 || horario.IntervaloFim.Hour > 14)
                    throw new ArgumentException("O intervalo só pode acontecer entre 12:00 e 14:00.");

                var duracaoIntervalo = horario.IntervaloFim - horario.IntervaloInicio;
                if (duracaoIntervalo.TotalHours != 1 && duracaoIntervalo.TotalHours != 2)
                    throw new ArgumentException("O intervalo só pode ter duração de 1h ou 2h.");

                var todosHorarios = new[] {
                horario.HorarioInicio, horario.HorarioFim,
                horario.IntervaloInicio, horario.IntervaloFim
                };

                if (todosHorarios.Any(h => h.Minute != 0 || h.Second != 0))
                    throw new ArgumentException("Todos os horários precisam começar e finalizar em horas fechadas (exemplo: 08:00).");

                if (horario.HorarioInicio >= horario.HorarioFim)
                    throw new ArgumentException("O horário de início precisa ser anterior ao horário de fim.");

                if (horario.IntervaloInicio >= horario.IntervaloFim)
                    throw new ArgumentException("O início do intervalo precisa ser anterior ao fim.");

                bool sucesso = await _repository.AdicionarHorarioAsync(horario);

                if (sucesso)
                {
                    retorno = "Horário adicionado com sucesso!";
                }

                return retorno;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region Excluir Horário 
        public async Task<string> ExcluirHorarioAsync(int id)
        {
            string retorno = "";
            try
            {
                var horario = await _repository.BuscarHorarioPorIdAsync(id);
                if (horario == null)
                    throw new ArgumentException("Horário não encontrado.");

                bool sucesso = await _repository.ExcluirHorarioAsync(id);

                if (sucesso)
                {
                    retorno = "Horário excluído com sucesso!";
                }

                return retorno;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}
