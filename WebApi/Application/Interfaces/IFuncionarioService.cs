using Domain.Dtos;
using Domain.Models;

namespace Application.Interfaces;

public interface IFuncionarioService
{
    Task<RetornoPaginado<Funcionario>> BuscarFuncionariosPaginadoAsync(int pagina, int quantidade);
    Task<List<Funcionario>> BuscarFuncionariosAsync();
    Task<List<Funcionario>> BuscarFuncionariosPorCPFAsync(string CPF);
    Task<Funcionario> BuscarFuncionarioPorIdAsync(int id);
    Task<bool> AdicionarFuncionarioAsync(FuncionarioDto funcionarioDto);
    Task<bool> AtualizarFuncionarioAsync(int id, FuncionarioDto funcionarioDto);
    Task<bool> DesativarFuncionarioAsync(int id);
}
