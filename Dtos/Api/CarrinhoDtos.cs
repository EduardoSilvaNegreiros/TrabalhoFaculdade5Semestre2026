namespace WebApplication1.Dtos.Api;

public sealed record CarrinhoAdicionarRequest(int ProdutoId, int Quantidade = 1);

public sealed record CarrinhoAtualizarRequest(int Quantidade);

public sealed record CarrinhoItemResponse(
    int ProdutoId,
    string Nome,
    string Categoria,
    decimal Preco,
    int Quantidade,
    decimal Subtotal,
    int LojistaId,
    string NomeLojista,
    decimal PercentualComissao,
    decimal ValorComissao,
    decimal ValorRepasseLojista);

public sealed record CarrinhoResponse(
    IReadOnlyList<CarrinhoItemResponse> Itens,
    int QuantidadeTotal,
    decimal Subtotal,
    decimal FreteEstimado,
    decimal TotalEstimado);

