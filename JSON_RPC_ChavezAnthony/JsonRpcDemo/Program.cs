using JsonRpcDemo.Data;
using JsonRpcDemo.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 👇 Estas líneas van ANTES de builder.Build()
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Postgres")));

builder.Services.AddScoped<ProductoService>();
builder.Services.AddControllers();

var app = builder.Build(); // 👈 Esto debe ir después

app.MapControllers();
app.Run();
