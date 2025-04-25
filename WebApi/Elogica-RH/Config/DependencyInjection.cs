using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using Application.Interfaces;
using Application.Services;
using AutoMapper;
using Application;

namespace Infrastructure.Config
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperConfig());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);



            #region Ferias

            //Registro dos serviços
            services.AddScoped<IFeriasService, FeriasService>();


            //Registro dos repositórios
            services.AddScoped<IFeriasRepository, FeriasRepository>();
            services.AddScoped<IFuncionarioRepository, FuncionarioRepository>();
            #endregion

            return services;
        }
    }
}