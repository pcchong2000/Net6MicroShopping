using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;
using System.Reflection;
using MediatR;
using Shopping.Api.Member;
using Shopping.Framework.Application;
using Shopping.Framework.EFCore.Members;
using Shopping.Framework.Web;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddDapr();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplication();
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

//add-migration init -Context MemberDbContext -OutputDir Members/migrations

// ���EFCore
builder.Services.AddWebDbContext<MemberDbContext>(builder.Configuration["ConnectionString"]);
// �����֤
builder.Services.AddAuthentication(JwtBearerIdentity.MemberScheme).AddTenantJwtBearer(builder.Configuration).AddMemberJwtBearer(builder.Configuration);
// �����Ȩ
builder.Services.AddWebAuthorization(builder.Configuration["ApiName"]);
// ��ӿ���
builder.Services.AddWebCors();

builder.Services.AddWebFreamework();

builder.Services.AddWebDataSeed<DataSeed>();

var app = builder.Build();

//��ʼ�����ݿ������
await app.Services.RunWebDataMigrate<MemberDbContext>();
await app.Services.RunWebDataSeed();


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
