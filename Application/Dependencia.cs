using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Services;
using Application.Services.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class Dependencia
    {
        public static void inyectarDependencias(this IServiceCollection services)
        {
            services.AddScoped<IRolService, RolService>();
            services.AddScoped<IDashBoardService , DashBoardService>();
            services.AddScoped<IHabitacionService , HabitacionService>();
            services.AddScoped<IMenuService , MenuService>();
            services.AddScoped<IReservaService , ReservaService>();
            services.AddScoped<ITipoHabitacionService , TipoHabitacionService >();
            services.AddScoped<IUsuarioService , UsuarioService >();
            services.AddScoped<IClienteService , ClienteService >();

        }

    }
}
