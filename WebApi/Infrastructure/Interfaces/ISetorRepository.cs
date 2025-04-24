using Domain;
using Domain.Models;

namespace Infrastructure.Interfaces;

public interface ISetorRepository
{
    Task<IEnumerable<Setor>> BuscarSetoresAsync();
    Task<RetornoPaginado<Setor>> BuscarSetoresPaginadoAsync(int pagina, int quantidade);
    Task<Setor> BuscarSetoresPorIdAsync(int id);
    Task<bool> AdicionarSetoresAsync(Setor setor);
    Task<bool> AtualizarSetoresAsync(Setor setor);
    Task<bool> ExcluirSetoresAsync(int id);
  
}
