using MediatR;
using MicroShoping.Application;
using MicroShoping.EFCore.Tenants;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using Tenant.Api;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplication();

//add-migration init -Context TenantDbContext -OutputDir Tenants/migrations

builder.Services.AddDbContext<TenantDbContext>(options =>
{
    string connectionString = builder.Configuration["ConnectionString"];
    //options.UseMySql(connectionString, ServerVersion.Parse("8.0"));
    options.UseSqlServer(connectionString);
});
builder.Services.AddScoped<DataSeed>();
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

// accepts any access token issued by identity server
builder.Services.AddAuthentication()
    .AddJwtBearer("TenantBearer", options =>
    {
        options.Authority = builder.Configuration["TenantIdentityServerUrl"];
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false
        };
    });

// adds an authorization policy to make sure the token is for scope 'api1'
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ApiScope", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim("scope", "tenantapi");
    });
});
// Ìí¼Ó¿çÓò
builder.Services.AddCors(options =>
{
    options.AddPolicy("any", policy =>
    {
        policy.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();

    });
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbcontext = scope.ServiceProvider.GetRequiredService<TenantDbContext>();
    await dbcontext.Database.MigrateAsync();

    var dataSeed = scope.ServiceProvider.GetRequiredService<DataSeed>();
    await dataSeed.Init(); 
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers().RequireAuthorization();

app.Run();
