using System.Threading.Channels;

namespace LojaMonolito.Mensageria;

public record PedidoCriado(int PedidoId, int ProdutoId, int Qtd);

public class FilaPedidos
{
    private readonly Channel<PedidoCriado> _canal =
        Channel.CreateUnbounded<PedidoCriado>();

    public ValueTask PublicarAsync(PedidoCriado e) =>
        _canal.Writer.WriteAsync(e);

    public IAsyncEnumerable<PedidoCriado> ConsumirAsync(CancellationToken ct) =>
        _canal.Reader.ReadAllAsync(ct);
}