// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;
using System;
using Microsoft.Extensions.Hosting;
using System.IO;
using System.Threading.Tasks;
using Google.Api;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Shopping.Framework.Web;
using Shopping.Framework.AccountApplication;
using IdentityServer4;
using Microsoft.AspNetCore.Http;
using Shopping.Api.IdentityMember.Data;

namespace Shopping.Api.IdentityMember
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews(options => {
                options.Filters.Add<ResponseFilter>();
            });

            builder.Services.AddIdentityServer(options => {
                //docker 中nginx 使用 http://shopping.api.identitymember 访问获取的地址与JWT携带不一致
                options.IssuerUri = builder.Configuration["IssuerUri"];
            })
                .AddInMemoryIdentityResources(Config.IdentityResources)
                .AddInMemoryApiScopes(Config.ApiScopes)
                .AddInMemoryClients(Config.Clients(builder.Configuration))
                .AddResourceOwnerValidator<ResourceOwnerPasswordValidator>()
                .AddProfileService<ProfileService>()
                .AddDeveloperSigningCredential();

            builder.Services.AddAccountApplication();

            builder.Services.AddWebFreamework();

            builder.Services.AddWebDbContext<MemberDbContext>(builder.Configuration["ConnectionString"]);

            builder.Services.AddWebDataSeed<DataSeed>();

            builder.Services.AddWebCors();

            builder.Services.AddSameSiteCookiePolicy();

            builder.Services.AddAuthentication().AddLocalApi(JwtBearerIdentity.MemberScheme, options => {
                options.ExpectedScope = "memberapi";
            }).AddTenantJwtBearer(builder.Configuration);

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy(IdentityServerConstants.LocalApi.PolicyName, policy =>
                {
                    policy.AddAuthenticationSchemes(JwtBearerIdentity.MemberScheme);
                    policy.RequireAuthenticatedUser();
                });
            });

            var app = builder.Build();

            //初始化数据库和数据
            await app.Services.RunWebDataMigrate<MemberDbContext>();
            await app.Services.RunWebDataSeed();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseCookiePolicy();

            app.UseStaticFiles();

            app.UseCors("any");

            app.UseRouting();

            app.UseIdentityServer();

            //eShopDapr的解决方案，UseIdentityServer在Routing 之前
            //app.UseCookiePolicy(new CookiePolicyOptions { MinimumSameSitePolicy = SameSiteMode.Lax });

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }

    }
}