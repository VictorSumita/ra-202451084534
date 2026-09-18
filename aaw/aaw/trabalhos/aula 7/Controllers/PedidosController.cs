using ApiVazada.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ApiVazada.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PedidosController : ControllerBase
{
    private int UsuarioLogadoId =>
        int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
    private readonly AppDbContext _db;

    public PedidosController(AppDbContext db) => _db = db;

    [Authorize]
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var pedido = _db.Pedidos.Find(id);
        if (pedido is null) return NotFound();

        if (pedido.UsuarioId != UsuarioLogadoId)
            return Forbid(); // 403 — autenticado, mas sem direito

        return Ok(pedido);
    }

    [Authorize]
    [HttpGet("usuario/{usuarioId}")]
    public IActionResult GetByUsuario(int usuarioId)
    {
        if (usuarioId != UsuarioLogadoId) return Forbid();
        var pedidos = _db.Pedidos.Where(p => p.UsuarioId == usuarioId).ToList();
        return Ok(pedidos);
    }
}