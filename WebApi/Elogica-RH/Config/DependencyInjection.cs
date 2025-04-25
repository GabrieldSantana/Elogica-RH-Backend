



using Application.Interfaces;
using Application.Services;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;

namespace Elogica_RH.Config;

public static class DependencyInjection
{
    public static IServiceCollection DependencInjection(this IServiceCollection services, IConfiguration configuration)
    {
        
        services.AddScoped<INotificador, Notificador>();

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
        #region Services
        services.AddScoped<ICargosSetoresService, CargosSetoresService>();
        #endregion
        #region Repository
        services.AddScoped<ICargosSetoresRepository, CargosSetoresRepository>();
            #endregion
        #endregion

        return services;
    }
}
