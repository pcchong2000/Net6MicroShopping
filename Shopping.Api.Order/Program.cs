using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using System.Linq;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using System.Reflection;
using MediatR;
using Shopping.Api.Order.Data;
using Shopping.Framework.Web;

namespace Shopping.Api.Order
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers().AddDapr();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddMediatR(Assembly.GetExecutingAssembly());


            //add-migration init -Context OrderDbContext -OutputDir Data/migrations

            // 添加EFCore
            builder.Services.AddWebDbContext<OrderDbContext>(builder.Configuration["ConnectionString"]);
            // 添加认证
            builder.Services.AddAuthentication(JwtBearerIdentity.TenantBearer)
                .AddTenantJwtBearer(builder.Configuration)
                .AddMemberJwtBearer(builder.Configuration);
            // 添加授权
            builder.Services.AddWebAuthorization(builder.Configuration["ApiName"]);
            // 添加跨域
            builder.Services.AddWebCors();

            builder.Services.AddWebFreamework();

            var app = builder.Build();

            //初始化数据库和数据
            await app.Services.RunWebDataMigrate<OrderDbContext>();


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
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
