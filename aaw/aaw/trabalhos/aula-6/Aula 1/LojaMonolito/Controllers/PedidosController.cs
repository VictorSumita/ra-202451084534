using LojaMonolito.Mensageria;
using Microsoft.AspNetCore.Mvc;

namespace LojaMonolito.Controllers;

[ApiController]
[Route("api/pedidos")]
public class PedidosController : ControllerBase
{
    private readonly FilaPedidos _fila;

    public PedidosController(FilaPedidos fila)
    {
        _fila = fila;
    }

    [HttpPost]
    public async Task<ActionResult> Criar(Pedido pedido)
    {
        await _fila.PublicarAsync(
            new PedidoCriado(pedido.Id, pedido.ProdutoId, pedido.Qtd));

        return Accepted($"api/pedidos/{pedido.Id}");
    }
}

public record Pedido(int Id, int ProdutoId, int Qtd);