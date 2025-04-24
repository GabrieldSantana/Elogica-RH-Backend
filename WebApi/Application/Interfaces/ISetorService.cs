using Domain.Dtos;
using Domain.Models;

namespace Application.Interfaces;

public interface ISetorService
{
    Task<IEnumerable<Setor>> BuscarSetoresAsync();
    Task<RetornoPaginado<Setor>>BuscarSetoresPaginadoAsync(int pagina, int quantidade);
    Task<Setor> BuscarSetorPorIdAsync(int id);
    Task<bool> AdicionarSetoresAsync(SetorDto setor);
    Task<bool> AtualizarSetoresAsync(SetorDto setor);
    Task<bool> ExcluirSetoresAsync(int id);

}
