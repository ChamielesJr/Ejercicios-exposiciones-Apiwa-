using GrpcProductServer.Services;
using GrpcProductServer.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 👉 Registrar el contexto de base de datos y el repositorio
builder.Services.AddDbContext<ProductContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ProductRepository>();

// 👉 Registrar el servicio gRPC
builder.Services.AddGrpc();

var app = builder.Build();

// 👉 Mapear el servicio gRPC personalizado
app.MapGrpcService<ProductCrudServiceImpl>();

// 👉 Endpoint básico HTTP
app.MapGet("/", () => "Use a gRPC client to communicate with this service.");

app.Run();
