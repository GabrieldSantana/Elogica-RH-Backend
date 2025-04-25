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
        
        services.AddScoped<IHorariosRepository, HorariosRepository>();
        services.AddScoped<IHorariosService, HorariosService>();

        #region Cargos
        #region Service
        services.AddScoped<ICargosServices, CargosService>();
        #endregion
        #region Repository
        services.AddScoped<ICargosRepository, CargosRepository>();
        #endregion
        #endregion

        #region Services
        services.AddScoped<ISetorService, SetorService>();
        #endregion

        #region Repositories
        services.AddScoped<ISetorRepository, SetorRepository>();
        #endregion

        return services;
    }
}
