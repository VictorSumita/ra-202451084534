using Microsoft.AspNetCore.Mvc;

namespace ServicoProdutos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutosController : ControllerBase
    {
        public static readonly List<Produto> Produtos = new()
        {
            new(1, "Clean Architecture", 89.90m, 12),
            new(2, "Domain-Driven Design", 120.00m, 5)
        };

        [HttpGet("{id}")]
        public ActionResult<Produto> GetById(int id)
        {
            return Produtos.FirstOrDefault(p => p.Id == id) is Produto p
                ? Ok(p)
                : NotFound();
        }
    }

    public record Produto(
        int Id,
        string Nome,
        decimal Preco,
        int Estoque
    );
}