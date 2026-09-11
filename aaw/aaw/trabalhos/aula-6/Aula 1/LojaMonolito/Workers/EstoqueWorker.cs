using LojaMonolito.Mensageria;

namespace LojaMonolito.Workers;

public class EstoqueWorker : BackgroundService
{
    private readonly FilaPedidos _fila;
    private readonly ILogger<EstoqueWorker> _log;

    public EstoqueWorker(FilaPedidos fila, ILogger<EstoqueWorker> log)
    {
        _fila = fila;
        _log = log;
    }

    protected override async Task ExecuteAsync(CancellationToken ct)
    {
        await foreach (var e in _fila.ConsumirAsync(ct))
        {
            _log.LogInformation("Baixando estoque do produto {Id}", e.ProdutoId);
            // aqui: chamada ao ServicoProdutos para baixar o estoque
        }
    }
}