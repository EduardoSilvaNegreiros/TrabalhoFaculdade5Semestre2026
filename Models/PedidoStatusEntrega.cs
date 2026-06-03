namespace WebApplication1.Models;

public static class PedidoStatusEntrega
{
    public const string Separacao = "Separação pelo lojista";
    public const string Enviado = "Enviado";
    public const string Entregue = "Entregue";

    public static readonly string[] Todos = { Separacao, Enviado, Entregue };
}
