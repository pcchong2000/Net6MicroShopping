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
using Microsoft.AspNetCore.Builder;
using Shopping.Framework.Web;
using System.Reflection;
using Google.Api;
using Microsoft.Extensions.Configuration;
using Shopping.Api.IdentityTenant.Data;
using Microsoft.Extensions.DependencyInjection;
using Shopping.Framework.AccountApplication;
using IdentityServer4;
using Microsoft.AspNetCore.Http;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using Shopping.Api.IdentityTenant.IdentityServerConfig;
using Shopping.Framework.Common;

namespace Shopping.Api.IdentityTenant
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews(options => {
                options.Filters.Add<ResponseFilter>();
            }).AddDapr();

            //add-migration init -Context TenantDbContext -OutputDir Data/migrations
            builder.Services.AddWebDbContext<TenantDbContext>(builder.Configuration["ConnectionString"]);
            builder.Services.AddWebDataSeed<DataSeed>();


            builder.Services.AddIdentityServer(options => {
                //LocalApi时，docker 中nginx 使用 http://shopping.api.identitymember 访问获取的地址与JWT携带不一致
                options.IssuerUri = builder.Configuration["IssuerUri"];
            })
            .AddInMemoryIdentityResources(Config.IdentityResources)
            .AddInMemoryApiScopes(Config.ApiScopes)
            .AddInMemoryClients(Config.Clients(builder.Configuration))
            .AddResourceOwnerValidator<ResourceOwnerPasswordValidator>()
            .AddProfileService<ProfileService>()
            .AddDeveloperSigningCredential();

            //builder.Services.AddOidcStateDataFormatterCache(JwtBearerIdentity.MemberScheme);

            builder.Services.AddAuthentication().AddLocalApi(JwtBearerIdentity.TenantScheme, options => {
                options.ExpectedScope = "tenantapi";
            }).AddOpenIdConnect(JwtBearerIdentity.MemberScheme, "会员账号", options =>
            {
                options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
                options.SignOutScheme = IdentityServerConstants.SignoutScheme;
                
                options.Authority = builder.Configuration["MemberIdentityServerUrl"];
                options.ClientId = "tenantAdmin";
                options.ClientSecret = "secret";
                options.ResponseType = "code";
                options.RequireHttpsMetadata = false;
                options.SaveTokens = true;
                options.Scope.Add("orderapi");
            });

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy(IdentityServerConstants.LocalApi.PolicyName, policy =>
                {
                    policy.AddAuthenticationSchemes(JwtBearerIdentity.TenantScheme);
                    policy.RequireAuthenticatedUser();
                });
            });



            builder.Services.AddSameSiteCookiePolicy();
            builder.Services.AddAccountApplication();

            builder.Services.AddWebFreamework();

            builder.Services.AddWebCors();

            builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

            builder.Services.AddCommonAutoMapper(typeof(AutoMapperExtensions).Assembly, typeof(Program).Assembly);

            builder.Services.AddSwaggerAuth();

            var app = builder.Build();

            //初始化数据库和数据
            await app.Services.RunWebDataMigrate<TenantDbContext>();
            await app.Services.RunWebDataSeed();


            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCookiePolicy();

            app.UseStaticFiles();

            app.UseCors("any");

            app.UseRouting();

            app.UseIdentityServer();

            app.UseAuthorization();

            app.UseCloudEvents();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.MapSubscribeHandler();

            app.Run();
        }

    }
}