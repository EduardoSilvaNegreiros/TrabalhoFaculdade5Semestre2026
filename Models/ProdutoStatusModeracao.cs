namespace WebApplication1.Models;

public static class ProdutoStatusModeracao
{
    public const string Aprovado = "Aprovado";
    public const string Pendente = "Pendente";
    public const string Reprovado = "Reprovado";

    public static readonly string[] Todos = { Aprovado, Pendente, Reprovado };
}
