using Domain.Models;

namespace Infrastructure.Interfaces;

public interface IMenuRepository
{
    Task<List<Menu>> ListarMenuAsync();
    Task<Menu> BuscaMenuPorIdAsync(int id);
    Task<bool> CriarMenuAsync(Menu menu);
    Task<bool> AtualizarMenuAsync(Menu menu);
    Task<bool> ExcluirMenuAsync(int idg);
}


