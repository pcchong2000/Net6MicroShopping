using Microsoft.Extensions.DependencyInjection;
using Shopping.Identity.AccountApplication.AccountServices;

namespace Shopping.Identity.AccountApplication
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