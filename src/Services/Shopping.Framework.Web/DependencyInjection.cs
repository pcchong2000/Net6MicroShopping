using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Shopping.Framework.Web.AccountServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.Framework.Web
{
    public class JwtBearerIdentity
    {
        public const string MemberScheme = "member";
        public const string TenantScheme = "tenant";
    }
    public static class DependencyInjection
    {
        public static IServiceCollection AddWebFreamework(this IServiceCollection services)
        {
            services.AddScoped<IPasswordHandler, DefaultPasswordHandler>();
            services.AddScoped(typeof(IAccountManage<,>), typeof(DefaultAccountManage<,>));

            services.AddHttpContextAccessor();
            services.AddScoped<ICurrentUserService, CurrentUserService>();

            return services;
        }
        /// <summary>
        /// 更新数据库结构
        /// </summary>
        /// <typeparam name="TContext"></typeparam>
        /// <param name="services"></param>
        /// <returns></returns>
        public static async Task RunWebDataMigrate<TContext>(this IServiceProvider services) where TContext : DbContext
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
        public static async Task RunWebDataSeed(this IServiceProvider services)
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
        public static IServiceCollection AddWebDataSeed<TDataSeed>(this IServiceCollection services) where TDataSeed : class, IDataSeed
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
        public static IServiceCollection AddWebDbContext<TContext>(this IServiceCollection services, string connectionString) where TContext : DbContext
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
        /// 添加跨域
        /// </summary>
        /// <param name="services"></param>
        public static IServiceCollection AddWebCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("any", policy =>
                {
                    policy.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();

                });
            });
            return services;
        }

        /// <summary>
        /// 授权
        /// </summary>
        /// <param name="services"></param>
        /// <param name="apiName"></param>
        /// <param name="schemes"></param>
        public static IServiceCollection AddWebAuthorization(this IServiceCollection services, string apiName)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("ApiScope", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("scope", apiName);
                });
                //if (schemes.Count > 0)
                //{
                //    options.DefaultPolicy = new AuthorizationPolicyBuilder()
                //    .RequireAuthenticatedUser()
                //    .AddAuthenticationSchemes(schemes.ToArray())
                //    .Build();
                //}
            });
            return services;
        }
        /// <summary>
        /// 会员认证
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="configuration"></param>
        public static AuthenticationBuilder AddMemberJwtBearer(this AuthenticationBuilder builder, IConfiguration configuration)
        {
            builder.AddJwtBearer(JwtBearerIdentity.MemberScheme, options =>
            {
                //https://docs.microsoft.com/zh-cn/aspnet/core/security/authentication/policyschemes?view=aspnetcore-6.0
                //将身份验证操作转发到另一个方案
                //1. options.ForwardChallenge = JwtBearerIdentity.TenantBearer  固定转发
                //2. 动态转发
                options.ForwardDefaultSelector = ctx => {
                    return ctx.Request.Path.Value!.Contains(JwtBearerIdentity.TenantScheme) ? JwtBearerIdentity.TenantScheme : null;
                    };
                options.Authority = configuration["MemberIdentityServerUrl"];
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false
                };
            });
            return builder;
        }
        /// <summary>
        /// 商户认证
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="configuration"></param>
        public static AuthenticationBuilder AddTenantJwtBearer(this AuthenticationBuilder builder, IConfiguration configuration)
        {
            builder.AddJwtBearer(JwtBearerIdentity.TenantScheme, options =>
            {
                options.Authority = configuration["TenantIdentityServerUrl"];
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false
                };
            });
            return builder;
        }
        /// <summary>
        /// 商户认证
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="configuration"></param>
        public static IServiceCollection AddHttpAndGrpc(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddGrpc();

            services.Configure<KestrelServerOptions>(options =>
            {
                options.Listen(IPAddress.Any, Convert.ToInt32(configuration["HttpPort"]), listenOptions =>
                {
                    listenOptions.Protocols = HttpProtocols.Http1AndHttp2;
                });

                options.Listen(IPAddress.Any, Convert.ToInt32(configuration["GrpcPort"]), listenOptions =>
                {
                    listenOptions.Protocols = HttpProtocols.Http2;
                });
            });
            return services;
        }
    }
}
