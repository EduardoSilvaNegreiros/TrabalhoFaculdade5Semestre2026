using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Dtos.Api;
using WebApplication1.Models;

namespace WebApplication1.Services.Checkout;

public sealed class CheckoutFacade : ICheckoutFacade
{
    private readonly ApplicationDbContext _context;

    public CheckoutFacade(ApplicationDbContext context)
    {
        _context = context;
    }

    public decimal CalcularFrete(IReadOnlyCollection<CarrinhoItem> carrinho, string? cep)
    {
        if (!carrinho.Any())
        {
            return 0m;
        }

        var lojistas = carrinho.Select(i => i.LojistaId).Distinct().Count();
        var adicionalCep = string.IsNullOrWhiteSpace(cep) ? 0m : cep.LastOrDefault() % 3;
        return 8.90m + (lojistas * 4.50m) + adicionalCep;
    }

    public string CalcularPrazo(IReadOnlyCollection<CarrinhoItem> carrinho)
    {
        var lojistas = carrinho.Select(i => i.LojistaId).Distinct().Count();
        return $"{Math.Max(2, lojistas + 2)} a {Math.Max(4, lojistas + 5)} dias úteis";
    }

    public async Task<CheckoutResult> FinalizarAsync(string usuarioEmail, IReadOnlyCollection<CarrinhoItem> carrinho, CheckoutRequest request, CancellationToken cancellationToken)
    {
        if (!carrinho.Any())
        {
            return new CheckoutResult(false, "Carrinho vazio.", null, 0m, "0 dias úteis");
        }

        var ids = carrinho.Select(i => i.ProdutoId).ToList();
        var produtos = await _context.Produtos
            .Where(p => ids.Contains(p.Id))
            .ToDictionaryAsync(p => p.Id, cancellationToken);

        foreach (var item in carrinho)
        {
            if (!produtos.TryGetValue(item.ProdutoId, out var produto) || produto.Estoque < item.Quantidade)
            {
                return new CheckoutResult(false, $"Estoque insuficiente para {item.Nome}.", null, 0m, "0 dias úteis");
            }
        }

        var frete = CalcularFrete(carrinho, request.Cep);
        var prazo = CalcularPrazo(carrinho);
        var pedido = new Pedido
        {
            UsuarioEmail = usuarioEmail,
            MetodoPagamento = string.IsNullOrWhiteSpace(request.MetodoPagamento) ? "Pix" : request.MetodoPagamento,
            CepEntrega = string.IsNullOrWhiteSpace(request.Cep) ? "01001000" : request.Cep,
            Subtotal = carrinho.Sum(i => i.Subtotal),
            Frete = frete,
            Total = carrinho.Sum(i => i.Subtotal) + frete,
            Status = "Pedido confirmado"
        };

        foreach (var item in carrinho)
        {
            pedido.Itens.Add(new PedidoItem
            {
                ProdutoId = item.ProdutoId,
                LojistaId = item.LojistaId,
                NomeProduto = item.Nome,
                NomeLojista = item.NomeLojista,
                Quantidade = item.Quantidade,
                PrecoUnitario = item.Preco,
                PercentualComissao = item.PercentualComissao,
                ValorComissao = item.ValorComissao,
                ValorRepasseLojista = item.ValorRepasseLojista,
                CodigoRastreio = $"BM{DateTime.UtcNow:yyMMdd}{item.ProdutoId:0000}",
                StatusEntrega = "Separação pelo lojista"
            });

            produtos[item.ProdutoId].Estoque -= item.Quantidade;
        }

        _context.Pedidos.Add(pedido);
        await _context.SaveChangesAsync(cancellationToken);

        return new CheckoutResult(true, "Pedido confirmado.", pedido, frete, prazo);
    }
}
