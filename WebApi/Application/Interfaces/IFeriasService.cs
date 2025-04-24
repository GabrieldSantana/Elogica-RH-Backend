
using Domain;

namespace Application.Interfaces
{
    public interface IFeriasService
    {
        Task<IEnumerable<Ferias>> BuscarFeriasAsync();
        Task<RetornoPaginado<Ferias>> BuscarFeriasPaginadoAsync(int pagina, int quantidade);
        Task<Ferias> BuscarFeriasPorIdAsync(int id);
        Task<Ferias> AdicionarFeriasAsync(Ferias dto);
        Task AtualizarFeriasAsync(int id, Ferias dto);
        Task ExcluirFeriasAsync(int id);
    }
}