using System.Data;
using System.Data.Common;
using Dapper;
using Domain.Models;
using Infrastructure.Interfaces;

namespace Infrastructure.Repositories;

public class MenuRepository : IMenuRepository
{
    private readonly IDbConnection _connection;

    public MenuRepository(IDbConnection connection)
    {
        _connection = connection;
    }

    public async Task<List<Menu>> BuscarMenuAsync()
    {
        try
        {
            string sql = "SELECT * FROM MENU";
            var menus = await _connection.QueryAsync<Menu>(sql);
            return [..menus];
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<int> TotalMenuAsync()
    {
        try
        {
            string sql = "SELECT COUNT(*) FROM MENU";
            int totalMenu = await _connection.ExecuteAsync(sql);
            return totalMenu;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<List<Menu>> BuscarMenuPaginadoAsync(int pagina, int quantidade)
    {
        try
        {
            string sql = "SELECT * FROM Menu ORDER BY ID OFFSET @OFFSET ROWS FETCH NEXT @QUANTIDADE ROWS ONLY";

            var parameters = new
            {
                OFFSET = (pagina - 1) * quantidade,
                QUANTIDADE = quantidade
            };

            var result =  await _connection.QueryAsync<Menu>(sql, parameters);

            return result.ToList();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Menu> BuscaMenuPorIdAsync(int id)
    {
        try
        {
            string sql = @"SELECT TOP 1 * FROM MENU WHERE Id = @Id";
            var parametro = new { Id = id };
            var menu = await _connection.QueryFirstOrDefaultAsync<Menu>(sql, parametro);
            return menu!;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<bool> AdicionarMenuAsync(Menu menu)
    {
        try
        {
            string sql = "INSERT INTO MENU VALUES (@Titulo, @Descricao, @Url, @Icone, @Ordem, @MenuPaiId)";
            var parametros = new
            {
                Titulo = menu.Titulo,
                Descricao = menu.Descricao,
                Url = menu.Url,
                Icone = menu.Icone,
                Ordem = menu.Ordem,
                MenuPaiId = menu.MenuPaiId == 0 ? null : (int?)menu.MenuPaiId
            };
            
            var result = await _connection.ExecuteAsync(sql, parametros);
            return result > 0;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<bool> AtualizarMenuAsync(int id, Menu menu)
    {
        try
        {
            string sql = @"UPDATE MENU SET Titulo = @Titulo, Descricao = @Descricao, Url = @Url, Icone = @Icone, Ordem = @Ordem, MenuPaiId = @MenuPaiId WHERE Id = @Id";
            var parametros = new
            {
                Id = id,
                Titulo = menu.Titulo,
                Descricao = menu.Descricao,
                Url = menu.Url,
                Icone = menu.Icone,
                Ordem = menu.Ordem,
                MenuPaiId = menu.MenuPaiId == 0 ? null : (int?)menu.MenuPaiId
            };
            
            var result = await _connection.ExecuteAsync(sql, parametros);
            return result > 0;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<bool> ExcluirMenuAsync(int id)
    {
        try
        {
            string sql = @"DELETE  FROM Menu WHERE Id = @Id";

            var parametros = new 
            {
                ID = id
            };

            var result = await _connection.ExecuteAsync(sql, parametros);

            return result > 0;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public Task<List<Menu>> ListarMenuAsync()
    {
        throw new NotImplementedException();
    }

    public Task<bool> CriarMenuAsync(Menu menu)
    {
        throw new NotImplementedException();
    }

    public Task<bool> AtualizarMenuAsync(Menu menu)
    {
        throw new NotImplementedException();
    }
}