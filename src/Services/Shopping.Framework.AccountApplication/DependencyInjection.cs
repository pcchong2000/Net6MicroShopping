using Microsoft.Extensions.DependencyInjection;
using Shopping.Framework.AccountApplication.AccountServices;

namespace Shopping.Framework.AccountApplication
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAccountApplication(this IServiceCollection services)
        {
            services.AddScoped<IPasswordHandler, DefaultPasswordHandler>();
            services.AddScoped(typeof(IAccountManage<,>), typeof(DefaultAccountManage<,>));

            return services;
        }
    }
}