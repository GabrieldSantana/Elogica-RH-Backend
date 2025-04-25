using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using Application.Interfaces;
using Application.Services;

namespace Elogica_RH.Config;

public static class DependencyInjection
{
    public static IServiceCollection DependencInjection(this IServiceCollection services, IConfiguration configuration)
    {

        #region Menu
        services.AddScoped<IMenuRepository, MenuRepository>();
        services.AddScoped<IMenuService, MenuService>();
        #endregion

        #region Funcionário
        services.AddScoped<IFuncionarioRepository, FuncionarioRepository>();
        services.AddScoped<IFuncionarioService, FuncionarioService>();
        #endregion

        #region Horarios
        services.AddScoped<IHorariosRepository, HorariosRepository>();
        services.AddScoped<IHorariosService, HorariosService>();
        #endregion

        #region Ferias
        //services.AddScoped<IFeriasRepository, FeriasRepository>();
        //services.AddScoped<IFeriasService, FeriasService>();
        #endregion

        #region Cargos
        #region Service
        services.AddScoped<ICargosServices, CargosService>();
        #endregion
        #region Repository
        services.AddScoped<ICargosRepository, CargosRepository>();
        #endregion
        #endregion

        #region Setor
        #region Services
        services.AddScoped<ISetorService, SetorService>();
        #endregion
        #region Repositories
        services.AddScoped<ISetorRepository, SetorRepository>();
        #endregion
        #endregion

        #region Cargos Setores
        services.AddScoped<ICargosSetoresService, CargosSetoresService>();
        services.AddScoped<ICargosSetoresRepository, CargosSetoresRepository>();
        #endregion

        #region Notificador
        services.AddScoped<INotificador, Notificador>();
        #endregion


        return services;
    }
}
