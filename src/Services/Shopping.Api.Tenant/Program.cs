using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Shopping.Api.Tenant;
using Shopping.Framework.Application;
using Shopping.Framework.EFCore.Tenants;
using Shopping.Framework.Web;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddDapr();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplication();
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

//add-migration init -Context TenantDbContext -OutputDir Tenants/migrations

// ���EFCore
builder.Services.AddWebDbContext<TenantDbContext>(builder.Configuration["ConnectionString"]);
// �����֤
builder.Services.AddAuthentication(JwtBearerIdentity.TenantScheme).AddTenantJwtBearer(builder.Configuration);
// �����Ȩ
builder.Services.AddWebAuthorization(builder.Configuration["ApiName"]);

// ��ӿ���
builder.Services.AddWebCors();

builder.Services.AddWebFreamework();

builder.Services.AddWebDataSeed<DataSeed>();

var app = builder.Build();

//��ʼ�����ݿ������
await app.Services.RunWebDataMigrate<TenantDbContext>();
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

app.MapControllers().RequireAuthorization();
app.MapSubscribeHandler();

app.Run();
