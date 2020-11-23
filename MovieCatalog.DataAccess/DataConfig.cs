using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MovieCatalog.DataAccess.EF;
using MovieCatalog.DataAccess.Interfaces;
using MovieCatalog.DataAccess.Repositories;

namespace MovieCatalog.DataAccess
{
    public class DataConfig
    {
        public static void Configure(IServiceCollection services, IConfiguration configuration)
        {
            string connectionStr = configuration.GetConnectionString("MovieCatalog");
            services.AddDbContextPool<AppDbContext>(options =>
            {
                options.UseSqlServer(connectionStr);
            });
            services.AddScoped<IDataContext, DataContext>();
        }
    }
}
