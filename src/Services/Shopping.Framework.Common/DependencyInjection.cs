using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.Framework.Common
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCommon(this IServiceCollection services)
        {
            //services.AddAutoMapper(type2, type3);

            return services;
        }
        public static IServiceCollection AddCommonAutoMapper(this IServiceCollection services, Assembly profileAssembly, params Assembly[] assemblies)
        {
            AutoMapperExtensions.Assemblies = assemblies;

            services.AddAutoMapper(profileAssembly);
            
            return services;
        }
    }
}
