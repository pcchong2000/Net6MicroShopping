using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Shopping.Framework.EFCore
{
    public static class DependencyInjection
    {
        /// <summary>
        /// 更新数据库结构
        /// </summary>
        /// <typeparam name="TContext"></typeparam>
        /// <param name="services"></param>
        /// <returns></returns>
        public static async Task RunDataMigrate<TContext>(this IServiceProvider services) where TContext : DbContext
        {
            using (var scope = services.CreateScope())
            {
                var dbcontext = scope.ServiceProvider.GetRequiredService<TContext>();
                await dbcontext.Database.MigrateAsync();
            }
        }
        /// <summary>
        /// 初始化数据
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static async Task RunDataSeed(this IServiceProvider services)
        {
            using (var scope = services.CreateScope())
            {
                var dataSeed = scope.ServiceProvider.GetRequiredService<IDataSeed>();
                await dataSeed.Init();
            }
        }
        /// <summary>
        /// 添加数据初始化
        /// </summary>
        /// <typeparam name="TDataSeed"></typeparam>
        /// <param name="services"></param>
        public static IServiceCollection AddDataSeed<TDataSeed>(this IServiceCollection services) where TDataSeed : class, IDataSeed
        {
            services.AddScoped<IDataSeed, TDataSeed>();

            return services;
        }
        /// <summary>
        /// EFCore
        /// </summary>
        /// <typeparam name="TContext"></typeparam>
        /// <param name="services"></param>
        /// <param name="connectionString"></param>
        public static IServiceCollection AddMySqlDbContext<TContext>(this IServiceCollection services, string connectionString) where TContext : DbContext
        {
            services.AddDbContext<TContext>(options =>
            {
                options.UseMySql(connectionString, ServerVersion.Parse("8.0"), options => {
                    //options.EnableRetryOnFailure(5);

                });
                //options.UseSqlServer(connectionString);
                //options.UseInMemoryDatabase(connectionString);
            });
            return services;
        }
        /// <summary>
        /// EFCore
        /// </summary>
        /// <typeparam name="TContext"></typeparam>
        /// <param name="services"></param>
        /// <param name="connectionString"></param>
        public static IServiceCollection AddSqlServerDbContext<TContext>(this IServiceCollection services, string connectionString) where TContext : DbContext
        {
            services.AddDbContext<TContext>(options =>
            {
                options.UseMySql(connectionString, ServerVersion.Parse("8.0"), options => {
                    //options.EnableRetryOnFailure(5);

                });
                //options.UseSqlServer(connectionString);
                //options.UseInMemoryDatabase(connectionString);
            });
            return services;
        }
    }
}