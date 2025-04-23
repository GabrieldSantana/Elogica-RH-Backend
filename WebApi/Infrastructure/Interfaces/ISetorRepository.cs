using Domain.Models;

namespace Infrastructure.Interfaces;

public interface ISetorRepository
{
    Task<IEnumerable<Setor>> ObterTodosSetoresAsync();
    Task<RetornoPaginado<Setor>> ObterSetoresPorPagina(int pagina, int quantidade);
    Task<Setor> ObterSetoresPorIdAsync(int id);
    Task<Setor> AdicionarSetoresAsync(Setor setor);
    Task<Setor> AtualizarSetoresAsync(Setor setor);
    Task<bool> RemoverSetoresAsync(int id);
  
}
