using JsonRpcDemo.Data;
using JsonRpcDemo.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace JsonRpcDemo.Services;

public class ProductoService
{
    private readonly AppDbContext _db;

    public ProductoService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<List<Producto>> Listar()
    {
        return await _db.Productos.ToListAsync();
    }

    public async Task<Producto> Crear(Producto producto)
    {
        _db.Productos.Add(producto);
        await _db.SaveChangesAsync();
        return producto;
    }

    public async Task<Producto?> ObtenerPorId(int id)
    {
        return await _db.Productos.FindAsync(id);
    }

    public async Task<bool> Eliminar(int id)
    {
        var producto = await _db.Productos.FindAsync(id);
        if (producto is null) return false;

        _db.Productos.Remove(producto);
        await _db.SaveChangesAsync();
        return true;
    }
}
