using Domain.Dtos;
using Domain.Models;


namespace Infrastructure.Interfaces
{
    public interface IFeriasRepository
    {
        Task<IEnumerable<Ferias>> BuscarFeriasAsync();
        Task<RetornoPaginado<Ferias>> BuscarFeriasPaginadoAsync(int pagina, int quantidade);
        Task<Ferias> BuscarFeriasPorIdAsync(int id);
        Task<bool> AdicionarFeriasAsync(FeriasDto dto);
        Task AtualizarFeriasAsync(Ferias ferias);
        Task ExcluirFeriasAsync(int id);
        
    }
}
