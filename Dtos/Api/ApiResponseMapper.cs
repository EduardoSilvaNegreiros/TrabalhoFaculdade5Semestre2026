using WebApplication1.Models;

namespace WebApplication1.Dtos.Api;

public static class ApiResponseMapper
{
    public static CarrinhoItemResponse ToResponse(this CarrinhoItem item)
    {
        return new CarrinhoItemResponse(
            item.ProdutoId,
            item.Nome,
            item.Categoria,
            item.Preco,
            item.Quantidade,
            item.Subtotal,
            item.LojistaId,
            item.NomeLojista,
            item.PercentualComissao,
            item.ValorComissao,
            item.ValorRepasseLojista);
    }

    public static PedidoResponse ToResponse(this Pedido pedido)
    {
        return new PedidoResponse(
            pedido.Id,
            pedido.UsuarioEmail,
            pedido.CriadoEm,
            pedido.MetodoPagamento,
            pedido.CepEntrega,
            pedido.Subtotal,
            pedido.Frete,
            pedido.Total,
            pedido.Status,
            pedido.Itens.Select(i => i.ToResponse()).ToList());
    }

    public static PedidoItemResponse ToResponse(this PedidoItem item)
    {
        return new PedidoItemResponse(
            item.Id,
            item.ProdutoId,
            item.LojistaId,
            item.NomeProduto,
            item.NomeLojista,
            item.Quantidade,
            item.PrecoUnitario,
            item.ValorComissao,
            item.ValorRepasseLojista,
            item.CodigoRastreio,
            item.StatusEntrega);
    }

    public static AvaliacaoResponse ToResponse(this Avaliacao avaliacao)
    {
        return new AvaliacaoResponse(
            avaliacao.Id,
            avaliacao.ProdutoId,
            avaliacao.UsuarioEmail,
            avaliacao.Nota,
            avaliacao.Comentario,
            avaliacao.MidiaUrl,
            avaliacao.TipoPele,
            avaliacao.TipoCabelo,
            avaliacao.CriadoEm);
    }
}

