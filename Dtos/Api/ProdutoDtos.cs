using WebApplication1.Models;

namespace WebApplication1.Dtos.Api;

public sealed record ProdutoResumoResponse(
    int Id,
    string Nome,
    string Categoria,
    string Marca,
    decimal Preco,
    int Estoque,
    string ImagemUrl,
    string TipoPele,
    string TipoCabelo,
    string Tom,
    bool Vegano,
    string? Lojista);

public sealed record ProdutoDetalheResponse(
    int Id,
    string Nome,
    string Descricao,
    string Categoria,
    string Marca,
    decimal Preco,
    int Estoque,
    string ImagemUrl,
    string TipoPele,
    string TipoCabelo,
    string CurvaturaCachos,
    string Tom,
    string Acabamento,
    bool Vegano,
    string Composicao,
    int LojistaId,
    string? Lojista);

public sealed record AvaliacaoResponse(
    int Id,
    int ProdutoId,
    string UsuarioEmail,
    int Nota,
    string Comentario,
    string MidiaUrl,
    string TipoPele,
    string TipoCabelo,
    DateTime CriadoEm);

public sealed record ProdutoRecomendadoResponse(
    int Id,
    string Nome,
    string Categoria,
    string Marca,
    decimal Preco,
    string Justificativa);

public static class ProdutoApiMapper
{
    public static ProdutoResumoResponse ToResumoResponse(this Produto produto)
    {
        return new ProdutoResumoResponse(
            produto.Id,
            produto.Nome,
            produto.Categoria,
            produto.Marca,
            produto.Preco,
            produto.Estoque,
            produto.ImagemUrl,
            produto.TipoPele,
            produto.TipoCabelo,
            produto.Tom,
            produto.Vegano,
            produto.Lojista?.NomeFantasia);
    }

    public static ProdutoDetalheResponse ToDetalheResponse(this Produto produto)
    {
        return new ProdutoDetalheResponse(
            produto.Id,
            produto.Nome,
            produto.Descricao,
            produto.Categoria,
            produto.Marca,
            produto.Preco,
            produto.Estoque,
            produto.ImagemUrl,
            produto.TipoPele,
            produto.TipoCabelo,
            produto.CurvaturaCachos,
            produto.Tom,
            produto.Acabamento,
            produto.Vegano,
            produto.Composicao,
            produto.LojistaId,
            produto.Lojista?.NomeFantasia);
    }
}

