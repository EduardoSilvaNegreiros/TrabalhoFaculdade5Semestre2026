namespace WebApplication1.Dtos.Api;

public sealed record CheckoutRequest(string MetodoPagamento = "Pix", string Cep = "01001000");

public sealed record CheckoutResponse(
    int PedidoId,
    string Status,
    decimal Subtotal,
    decimal Frete,
    decimal Total,
    string Prazo,
    IReadOnlyList<PedidoItemResponse> Itens);

public sealed record PedidoResponse(
    int Id,
    string UsuarioEmail,
    DateTime CriadoEm,
    string MetodoPagamento,
    string CepEntrega,
    decimal Subtotal,
    decimal Frete,
    decimal Total,
    string Status,
    IReadOnlyList<PedidoItemResponse> Itens);

public sealed record PedidoItemResponse(
    int Id,
    int ProdutoId,
    int LojistaId,
    string NomeProduto,
    string NomeLojista,
    int Quantidade,
    decimal PrecoUnitario,
    decimal ValorComissao,
    decimal ValorRepasseLojista,
    string CodigoRastreio,
    string StatusEntrega);

