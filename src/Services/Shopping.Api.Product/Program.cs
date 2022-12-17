using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Shopping.Api.Product.Data;
using Shopping.Api.Product.Grpc.Services;
using Shopping.Framework.Common;
using Shopping.Framework.Web;
using System.Net;
using System.Reflection;

namespace Shopping.Api.Product
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers(options =>
            {
                options.Filters.Add<ResponseFilter>();
            }).AddDapr();

            //add-migration init -Context ProductDbContext -OutputDir Data/migrations
            // 添加EFCore
            builder.Services.AddWebDbContext<ProductDbContext>(builder.Configuration["ConnectionString"]!);
            builder.Services.AddWebDataSeed<DataSeed>();

            // 添加认证
            builder.Services.AddAuthentication(JwtBearerIdentity.MemberScheme)
            .AddTenantJwtBearer(builder.Configuration)
            .AddMemberJwtBearer(builder.Configuration);
            // 添加授权
            builder.Services.AddWebAuthorization(builder.Configuration["ApiName"]!);

            builder.Services.AddWebFreamework();

            builder.Services.AddWebCors();

            builder.Services.AddHttpAndGrpc(builder.Configuration);

            builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

            builder.Services.AddCommonAutoMapper(typeof(AutoMapperExtensions).Assembly, typeof(Program).Assembly);

            builder.Services.AddSwaggerAuth();

            var app = builder.Build();

            //初始化数据库和数据
            await app.Services.RunWebDataMigrate<ProductDbContext>();
            await app.Services.RunWebDataSeed();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseApiBaseException();
            app.UseCloudEvents();

            app.UseCors("any");
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers().RequireAuthorization();
            app.MapGrpcService<ProductListService>();
            app.MapSubscribeHandler();

            app.Run();
        }
    }
}
