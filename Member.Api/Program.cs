using MicroShoping.EFCore.Members;
using Microsoft.EntityFrameworkCore;
using MicroShoping.Application;
using Microsoft.IdentityModel.Tokens;
using Member.Api;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddDapr();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplication();

// accepts any access token issued by identity server
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.Authority = builder.Configuration["IdentityServerUrl"];
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
        policy.RequireClaim("scope", "memberapi");
    });
});
// Ìí¼Ó¿çÓò
builder.Services.AddCors(options => {
    options.AddPolicy("any", policy => {
        policy.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();

    });
});

//add-migration init -Context MemberDbContext -OutputDir Members/migrations

builder.Services.AddDbContext<MemberDbContext>(options =>
{
    string connectionString = builder.Configuration["ConnectionString"];
    //options.UseMySql(connectionString, ServerVersion.Parse("8.0"));
    options.UseSqlServer(connectionString);
});
builder.Services.AddScoped<DataSeed>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbcontext = scope.ServiceProvider.GetRequiredService<MemberDbContext>();
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
app.UseCors("any");
app.UseAuthentication();
app.UseAuthorization();
app.UseCloudEvents();

app.MapControllers();
app.MapSubscribeHandler();

app.Run();
