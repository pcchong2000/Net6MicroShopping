using Microsoft.EntityFrameworkCore;
using Product.Api.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//add-migration init -Context ProductDbContext -OutputDir Data/migrations
builder.Services.AddDbContext<ProductDbContext>(options =>
{
    string connectionString = builder.Configuration["ConnectionString"];
    //options.UseMySql(connectionString, ServerVersion.Parse("8.0"));
    options.UseSqlServer(connectionString);
});



var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbcontext = scope.ServiceProvider.GetRequiredService<ProductDbContext>();
    await dbcontext.Database.MigrateAsync();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
