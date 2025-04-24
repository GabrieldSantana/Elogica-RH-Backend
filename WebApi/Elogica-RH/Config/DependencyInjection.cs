using Application.Interfaces;
using Application.Services;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;

namespace Elogica_RH.Config;

public static class DependencyInjection
{
    public static IServiceCollection DependencInjection(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IFuncionarioRepository, FuncionarioRepository>();
        services.AddScoped<IFuncionarioService, FuncionarioService>();
        services.AddScoped<INotificador, Notificador>();

        #region CARGOS SETORES
        #region SERVICES
        services.AddScoped<ICargosSetoresService, CargosSetoresService>();
       

        #endregion

        #region REPOSITORY
        services.AddScoped<ICargosSetoresRepository, CargosSetoresRepository>();
        #endregion
        #endregion


        return services;
    }
}
