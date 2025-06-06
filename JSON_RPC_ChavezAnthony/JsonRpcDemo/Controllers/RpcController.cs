using Microsoft.AspNetCore.Mvc;
using JsonRpcDemo.Services;
using JsonRpcDemo.Data.Entities;
using System.Text.Json;

namespace JsonRpcDemo.Controllers;

[ApiController]
[Route("rpc")]
public class RpcController : ControllerBase
{
    private readonly ProductoService _productoService;

    public RpcController(ProductoService productoService)
    {
        _productoService = productoService;
    }

    [HttpPost]
    public async Task<IActionResult> HandleRpc([FromBody] JsonElement body)
    {
        var method = body.GetProperty("method").GetString();
        var id = body.GetProperty("id").GetInt32();
        var result = (object?)null;

        try
        {
            switch (method)
            {
                case "listar":
                    result = await _productoService.Listar();
                    break;

                case "crear":
                    var producto = JsonSerializer.Deserialize<Producto>(
                        body.GetProperty("params").GetRawText(),
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                    )!;
                    result = await _productoService.Crear(producto);
                    break;

                case "obtener":
                    var idObtener = body.GetProperty("params")[0].GetInt32();
                    result = await _productoService.ObtenerPorId(idObtener);
                    break;

                case "eliminar":
                    var idEliminar = body.GetProperty("params")[0].GetInt32();
                    result = await _productoService.Eliminar(idEliminar);
                    break;

                default:
                    return BadRequest(new
                    {
                        jsonrpc = "2.0",
                        error = new { code = -32601, message = "Método no encontrado" },
                        id
                    });
            }

            return Ok(new
            {
                jsonrpc = "2.0",
                result,
                id
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new
            {
                jsonrpc = "2.0",
                error = new { code = -32603, message = ex.Message },
                id
            });
        }
    }
}
