using System.Data;
using Dapper;
using Domain.Models;
using Infrastructure.Interfaces;

namespace Infrastructure.Repositories;

public class FuncionarioRepository : IFuncionarioRepository
{
    private readonly IDbConnection _connection;

    public FuncionarioRepository(IDbConnection connection)
    {
        _connection = connection;
    }

    public async Task<RetornoPaginado<Funcionario>> BuscarFuncionariosPaginadoAsync(int pagina, int quantidade)
    {
        try
        {
            string sql =
             @"SELECT * FROM Funcionarios
                ORDER BY Id
                OFFSET @OFFSET ROWS
                FETCH NEXT @FETCHNEXT ROWS ONLY";
            var parametros = new
            {
                OFFSET = (pagina - 1) * quantidade,
                FETCHNEXT = quantidade
            };
            var funcionariosPaginados = await _connection.QueryAsync<Funcionario>(sql, parametros);

            var sqlContagemFuncionarios = "SELECT COUNT(*) FROM Funcionarios";

            var totalFuncionarios = await _connection.QueryFirstOrDefaultAsync<int>(sqlContagemFuncionarios);

            return new RetornoPaginado<Funcionario>
            {
                TotalRegistro = totalFuncionarios,
                Registros = funcionariosPaginados.ToList()
            };
        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task<bool> AdicionarFuncionarioAsync(Funcionario funcionario)
    {
        try
        {
            var sql =
                @"INSERT INTO Funcionarios VALUES ( @NOME, @CPF, @DATANASCIMENTO, @EMAIL, @TELEFONE, @ENDERECO, @DATACONTRATACAO, @SALARIO, @ATIVO, @CARGOSID, @SETORESID, @HORARIOSID )";
            var parametros = new
            {
                NOME = funcionario.Nome,
                CPF = funcionario.CPF,
                DATANASCIMENTO = funcionario.DataNascimento,
                EMAIL = funcionario.Email,
                TELEFONE = funcionario.Telefone,
                ENDERECO = funcionario.Endereco,
                DATACONTRATACAO = funcionario.DataContratacao,
                SALARIO = funcionario.Salario,
                ATIVO = funcionario.Ativo,
                CARGOSID = funcionario.CargosId,
                SETORESID = funcionario.SetoresId,
                HORARIOSID = funcionario.HorariosId
            };

            var resultadoInsercao = await _connection.ExecuteAsync(sql, parametros);

            return resultadoInsercao > 0 ? true : false;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<Funcionario> BuscarFuncionarioPorIdAsync(int id)
    {
        try
        {
            string sql = $"SELECT TOP 1 * FROM Funcionarios WHERE Id = {id}";

            var funcionario = await _connection.QueryFirstOrDefaultAsync<Funcionario>(sql);

            return funcionario;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<List<Funcionario>> BuscarFuncionariosPorCPFAsync(string CPF)
    {
        try
        {
            var sql = $"SELECT * FROM Funcionarios WHERE CPF={CPF}";

            var funcionarios = await _connection.QueryAsync<Funcionario>(sql);

            return funcionarios.ToList();
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<List<Funcionario>> BuscarFuncionariosAsync()
    {
        try
        {
            string sql = "SELECT * FROM Funcionarios";
            var funcionarios = await _connection.QueryAsync<Funcionario>(sql);
            return funcionarios.ToList();
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<bool> AtualizarFuncionarioAsync(Funcionario funcionario)
    {
        try
        {
            var sql = @"
            UPDATE Funcionarios 
            SET 
            Nome=@NOME,
            CPF=@CPF,
            DataNascimento=@DATANASCIMENTO,
            Email=@EMAIL,
            Telefone=@TELEFONE,
            Endereco=@ENDERECO,
            DataContratacao=@DATACONTRATACAO,
            Salario=@SALARIO,
            Ativo=@ATIVO,
            CargosId=@CARGOSID,
            SetoresId=@SETORESID,
            HorariosId=@HORARIOSID
            WHERE Id=@ID";
            var parametros = new
            {
                NOME = funcionario.Nome,
                CPF = funcionario.CPF,
                DATANASCIMENTO = funcionario.DataNascimento,
                EMAIL = funcionario.Email,
                TELEFONE = funcionario.Telefone,
                ENDERECO = funcionario.Endereco,
                DATACONTRATACAO = funcionario.DataContratacao,
                SALARIO = funcionario.Salario,
                ATIVO = funcionario.Ativo,
                CARGOSID = funcionario.CargosId,
                SETORESID = funcionario.SetoresId,
                HORARIOSID = funcionario.HorariosId,
                ID = funcionario.Id
            };

            var resultadoAtualizacao = await _connection.ExecuteAsync(sql, parametros);

            return resultadoAtualizacao > 0 ? true : false;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<bool> DesativarFuncionarioAsync(int id)
    {
        try
        {
            string sql = $"UPDATE Funcionarios SET Ativo={0} WHERE Id={id}";

            var resultado = await _connection.ExecuteAsync(sql);

            return resultado > 0 ? true : false;
        }
        catch (Exception)
        {
            throw;
        }
    }
}
