using Google.Api;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security.Claims;
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
            services.AddHttpContextAccessor();
            services.AddScoped<ICurrentUserService, CurrentUserService>();

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
                
                //验证未通过之后执行其他方案的未通过流程，
                //比如你网站用cookie验证身份，但是只对接google用户登录，验证失败时应该直接去google的失败处理，此时就可以 ForwardChallenge="google"
                //options.ForwardChallenge = JwtBearerIdentity.TenantBearer

                //将身份验证操作转发到另一个方案
                //动态转发
                options.ForwardDefaultSelector = ctx => {
                    return ctx.Request.Path.Value!.Contains(JwtBearerIdentity.TenantScheme) ? JwtBearerIdentity.TenantScheme : null;
                    };
                options.Authority = configuration["MemberIdentityServerUrl"];
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false,
                };
                //另一设备登录挤退方案
                //登录后token写入此用户最后登录设备号(或者其他可确认为单次登录的信息),同时ProfileService缓存此用户最后登录设备号
                //在验证token 这里查询token和缓存是否一致，不一致返回认证失败；
                //options.Events = new Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerEvents
                //{
                //    OnTokenValidated= context =>
                //    {
                //        var currentLoginTime = context.Principal?.Claims.Where(a=>a.Type=="login_time").FirstOrDefault();
                //        if (currentLoginTime != null)
                //        {
                //            //var cache = context.HttpContext.RequestServices.GetRequiredService<Redis>();
                //            var loginTime = await cache.get("user_token_login_time");
                //            if (loginTime != null)
                //            {
                //                if (currentLoginTime < loginTime)//不是最新登录的token
                //                {
                //                    context.Fail("设备在其他地方登录");
                //                }
                //            }
                //            context.Success();
                //        }
                //        return Task.CompletedTask;
                //    }
                //};
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
        /// 添加服务监听
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="configuration"></param>
        public static IServiceCollection AddHttpAndGrpc(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddGrpc();

            services.Configure<KestrelServerOptions>(options =>
            {
                //nginx 走HttpPort
                options.Listen(IPAddress.Any, Convert.ToInt32(configuration["HttpPort"]), listenOptions =>
                {
                    listenOptions.Protocols = HttpProtocols.Http1AndHttp2;
                });
                //grpc 走GrpcPort
                options.Listen(IPAddress.Any, Convert.ToInt32(configuration["GrpcPort"]), listenOptions =>
                {
                    listenOptions.Protocols = HttpProtocols.Http2;
                });
            });
            return services;
        }

        public static IServiceCollection AddSwaggerAuth(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("bearerAuth", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Description = "JWT Authorization header using the Bearer scheme."
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "bearerAuth" }
                        },
                        new string[] {}
                    }
                });
            });
            return services;
        }
    }
}
