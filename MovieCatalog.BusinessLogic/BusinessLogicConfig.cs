using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MovieCatalog.DataAccess;

namespace MovieCatalog.BusinessLogic
{
    public class BusinessLogicConfig
    {
        public static void Configure(IServiceCollection services, IConfiguration configuration)
        {
            DataConfig.Configure(services, configuration);
        }
    }
}
