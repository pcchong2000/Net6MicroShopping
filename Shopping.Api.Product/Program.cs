using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Shopping.Api.Product;
using Shopping.Api.Product.Data;
using Shopping.Framework.Web;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddDapr();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

//add-migration init -Context ProductDbContext -OutputDir Data/migrations

// 添加EFCore
builder.Services.AddWebDbContext<ProductDbContext>(builder.Configuration["ConnectionString"]);
// 添加认证
builder.Services.AddAuthentication(JwtBearerIdentity.TenantBearer).AddTenantJwtBearer(builder.Configuration);
// 添加授权
builder.Services.AddWebAuthorization(builder.Configuration["ApiName"]);
// 添加跨域
builder.Services.AddWebCors();

builder.Services.AddWebFreamework();

builder.Services.AddWebDataSeed<DataSeed>();

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

app.UseCors("any");
app.UseAuthentication();
app.UseAuthorization();
app.UseCloudEvents();

app.MapControllers();
app.MapSubscribeHandler();

app.Run();
