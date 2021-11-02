using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Shopping.Api.Pay.Data;
using Shopping.Framework.Web;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddDapr();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

//add-migration init -Context PayDbContext -OutputDir Data/migrations

// ���EFCore
builder.Services.AddWebDbContext<PayDbContext>(builder.Configuration["ConnectionString"]);
// �����֤
builder.Services.AddAuthentication().AddTenantJwtBearer(builder.Configuration).AddMemberJwtBearer(builder.Configuration);
// �����Ȩ
builder.Services.AddWebAuthorization(builder.Configuration["ApiName"]);
// ��ӿ���
builder.Services.AddWebCors();

builder.Services.AddWebFreamework();

var app = builder.Build();

//��ʼ�����ݿ�
await app.Services.RunWebDataMigrate<PayDbContext>();


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
