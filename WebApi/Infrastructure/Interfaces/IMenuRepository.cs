using Domain.Models;

namespace Infrastructure.Interfaces;

public interface IMenuRepository
{
    Task<List<Menu>> BuscarMenuAsync();
    Task<int> TotalMenuAsync();
    Task<List<Menu>> BuscarMenuPaginadoAsync(int pagina, int quantidade);
    Task<Menu> BuscaMenuPorIdAsync(int id);
    Task<bool> AdicionarMenuAsync(Menu menu);
    Task<bool> AtualizarMenuAsync(int id, Menu menu);
    Task<bool> ExcluirMenuAsync(int idg);
}


