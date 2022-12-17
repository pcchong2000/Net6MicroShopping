using MediatR;
using Shopping.Framework.Common;
using Shopping.Framework.Web;
using System.Reflection;

namespace Shopping.Api.OSS
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

            // 添加认证
            builder.Services.AddAuthentication(JwtBearerIdentity.MemberScheme)
                .AddTenantJwtBearer(builder.Configuration)
                .AddMemberJwtBearer(builder.Configuration);
            // 添加授权
            builder.Services.AddWebAuthorization(builder.Configuration["ApiName"]!);

            builder.Services.AddWebFreamework();

            builder.Services.AddWebCors();

            builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

            //builder.Services.AddCommonAutoMapper(typeof(AutoMapperExtensions).Assembly, typeof(Program).Assembly);

            builder.Services.AddSwaggerAuth();

            var app = builder.Build();

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

            app.MapControllers();

            app.Run();
        }
    }
}