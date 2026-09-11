using Microsoft.AspNetCore.Mvc;

namespace LojaMonolito.Controllers;

[ApiController]
[Route("api/pedidos")]
public class PedidosController : ControllerBase
{
    private readonly IHttpClientFactory _factory;

    public PedidosController(IHttpClientFactory factory)
    {
        _factory = factory;
    }

    [HttpPost]
    public async Task<IActionResult> Criar([FromBody] CriarPedidoRequest request)
    {
        var client = _factory.CreateClient();
        var url = $"http://localhost:5101/api/produtos/{request.ProdutoId}";

        var response = await client.GetAsync(url);

        if (!response.IsSuccessStatusCode)
            return BadRequest($"Produto {request.ProdutoId} nao encontrado no ServicoProdutos.");

        var produto = await response.Content.ReadFromJsonAsync<ProdutoDto>();

        if (produto is null)
            return BadRequest("Resposta invalida do ServicoProdutos.");

        if (produto.Estoque < request.Quantidade)
            return BadRequest($"Estoque insuficiente. Disponivel: {produto.Estoque}");

        var pedido = new
        {
            PedidoId = Guid.NewGuid(),
            request.ProdutoId,
            produto.Nome,
            request.Quantidade,
            Total = produto.Preco * request.Quantidade
        };

        return Ok(pedido);
    }
}

public record CriarPedidoRequest(int ProdutoId, int Quantidade);
public record ProdutoDto(int Id, string Nome, decimal Preco, int Estoque);