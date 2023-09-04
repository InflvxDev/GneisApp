using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.DBcontext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Infrastructure.Repositories.Contracts;
using Infrastructure.Repositories;
using Infrastructure.Utility;

namespace Infrastructure
{
    public static class Dependencia
    {
        public static void inyectarDependencias(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<PostgresContext>(options =>{
                options.UseNpgsql(configuration.GetConnectionString("cadenaPostgres"));
            });

            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IReservaRepository, ReservaRepository>();

            services.AddAutoMapper(typeof(AutoMapperProfile));

        }

    }
}
