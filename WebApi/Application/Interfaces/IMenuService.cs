using Domain.Dtos;
using Domain.Models;

namespace Application.Interfaces;

public interface IMenuService
{
    Task<List<RespostaMenuDto>> BuscarMenuAsync();
    Task<RespostaMenuDto> BuscaMenuPorIdAsync(int id);
    Task<bool> AdicionarMenuAsync(CriaMenuDto menu);
    Task<bool> AtualizarMenuAsync(AtualizaMenuDto menu);
    Task<RetornoPaginado<RespostaMenuDto>> BuscarMenuPaginadoAsync(int pagina, int quantidade);
    Task<bool> ExcluirMenuAsync(int idg);
}