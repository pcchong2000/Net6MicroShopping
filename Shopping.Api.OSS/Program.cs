using Shopping.Framework.Web;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// �����֤
builder.Services.AddAuthentication(JwtBearerIdentity.MemberScheme)
    .AddTenantJwtBearer(builder.Configuration)
    .AddMemberJwtBearer(builder.Configuration);
// �����Ȩ
builder.Services.AddWebAuthorization(builder.Configuration["ApiName"]);
// ��ӿ���
builder.Services.AddWebCors();

builder.Services.AddWebFreamework();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("any");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
