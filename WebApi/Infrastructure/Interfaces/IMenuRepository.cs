using Domain.Models;

namespace Infrastructure.Interfaces;

public interface IMenuRepository
{
    Task<List<Menu>> BuscarMenuAsync();
    Task<Menu> BuscaMenuPorIdAsync(int id);
    Task<bool> AdicionarMenuAsync(Menu menu);
    Task<bool> AtualizarMenuAsync(Menu menu);
    Task<RetornoPaginado<Menu>> BuscarMenuPaginadoAsync();
    Task<bool> ExcluirMenuAsync(int idg);
}


