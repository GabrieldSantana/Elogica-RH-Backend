using System.Data.Common;
using Dapper;
using Domain.Models;
using Infrastructure.Interfaces;

namespace Infrastructure.Repositories;

public class MenuRepository: IMenuRepository
{
    private readonly DbConnection _connection;

    public MenuRepository(DbConnection connection)
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
                MenuPaiId = menu.MenuPaiId
            };
            
            var result = await _connection.ExecuteAsync(sql, parametros);
            return result > 0;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<bool> AtualizarMenuAsync(Menu menu)
    {
        try
        {
            string sql = @"UPDATE MENU SET Titulo = @Titulo, Descricao = @Descricao, Url = @Url, Icone = @Icone, Ordem = @Ordem, MenuPaiId = @MenuPaiId WHERE Id = @Id";
            var parametros = new
            {
                Id = menu.Id,
                Titulo = menu.Titulo,
                Descricao = menu.Descricao,
                Url = menu.Url,
                Icone = menu.Icone,
                Ordem = menu.Ordem,
                MenuPaiId = menu.MenuPaiId
            };
            
            var result = await _connection.ExecuteAsync(sql, parametros);
            return result > 0;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public Task<RetornoPaginado<Menu>> BuscarMenuPaginadoAsync()
    {
        throw new NotImplementedException();
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
}