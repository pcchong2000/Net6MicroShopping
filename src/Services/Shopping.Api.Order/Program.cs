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
using Shopping.Api.Order.Grpc.Services;
using Shopping.Framework.Common;

namespace Shopping.Api.Order
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers(options => {
                options.Filters.Add<ResponseFilter>();
            }).AddDapr();


            //add-migration init -Context OrderDbContext -OutputDir Data/migrations

            // 添加EFCore
            builder.Services.AddWebDbContext<OrderDbContext>(builder.Configuration["ConnectionString"]!);
            // 添加认证
            builder.Services.AddAuthentication(JwtBearerIdentity.MemberScheme)
                .AddTenantJwtBearer(builder.Configuration)
                .AddMemberJwtBearer(builder.Configuration);
            // 添加授权
            builder.Services.AddWebAuthorization(builder.Configuration["ApiName"]!);

            builder.Services.AddScoped<IProductService, ProductService>();

            builder.Services.AddWebFreamework();

            builder.Services.AddWebCors();

            builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

            builder.Services.AddCommonAutoMapper(typeof(AutoMapperExtensions).Assembly, typeof(Program).Assembly);

            builder.Services.AddSwaggerAuth();

            

            var app = builder.Build();

            //初始化数据库和数据
            await app.Services.RunWebDataMigrate<OrderDbContext>();

            
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
