using Microsoft.EntityFrameworkCore;
using SistemaControleGastosResidenciaisAPI;
using SistemaControleGastosResidenciaisAPI.Repositories;
using SistemaControleGastosResidenciaisAPI.Services;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string" + "'DefaultConnection' not found.");
builder.Services.AddDbContext <BaseContext>(option => option.UseSqlite(connectionString));

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddScoped<IPersonService, PersonService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
