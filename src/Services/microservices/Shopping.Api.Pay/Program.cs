using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Shopping.Api.Pay.Data;
using Shopping.Framework.Common;
using Shopping.Framework.EFCore;
using Shopping.Framework.Web;
using System.Reflection;

namespace Shopping.Api.Pay
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers(options =>
            {
                options.Filters.Add<ResponseFilter>();
            }).AddDapr();

            //add-migration init -Context PayDbContext -OutputDir Data/migrations

            // 添加EFCore
            builder.Services.AddMySqlDbContext<PayDbContext>(builder.Configuration["ConnectionString"]!);
            // 添加认证
            builder.Services.AddAuthentication(JwtBearerIdentity.MemberScheme)
                .AddTenantJwtBearer(builder.Configuration)
                .AddMemberJwtBearer(builder.Configuration);
            // 添加授权
            builder.Services.AddWebAuthorization(builder.Configuration["ApiName"]!);


            builder.Services.AddWebFreamework();

            builder.Services.AddWebCors();

            builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

            builder.Services.AddCommonAutoMapper(typeof(AutoMapperExtensions).Assembly, typeof(Program).Assembly);

            builder.Services.AddSwaggerAuth();

            var app = builder.Build();

            //初始化数据库
            await app.Services.RunDataMigrate<PayDbContext>();


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseApiBaseException();
            app.UseCors("any");
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCloudEvents();

            app.MapControllers();
            app.MapSubscribeHandler();

            app.Run();
        }
    }
}