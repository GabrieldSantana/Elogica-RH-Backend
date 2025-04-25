using Domain.Models;


namespace Infrastructure.Interfaces
{
    public interface IFeriasRepository
    {
        Task<IEnumerable<Ferias>> BuscarFeriasAsync();
        Task<RetornoPaginado<Ferias>> BuscarFeriasPaginadoAsync(int pagina, int quantidade);
        Task<Ferias> BuscarFeriasPorIdAsync(int id);
        Task<Ferias> AdicionarFeriasAsync(Ferias ferias);
        Task AtualizarFeriasAsync(Ferias ferias);
        Task ExcluirFeriasAsync(int id);
        Task<bool> FuncionarioTemFerias(int funcionarioId, DateTime dataInicio, DateTime dataFim);
    }
}