using Domain.Models;

namespace Infrastructure.Interfaces;

public interface IFuncionarioRepository
{
    Task<RetornoPaginado<Funcionario>>BuscarFuncionariosPaginadoAsync(int pagina, int quantidade);
    Task<List<Funcionario>> BuscarFuncionariosAsync();
    Task<List<Funcionario>> BuscarFuncionariosPorCPFAsync(string CPF);
    Task<Funcionario> BuscarFuncionarioPorIdAsync(int id);
    Task<bool> AdicionarFuncionarioAsync(Funcionario funcionario);
    Task<bool> AtualizarFuncionarioAsync(Funcionario funcionario);
    Task<bool> DesativarFuncionarioAsync(int id);
}
