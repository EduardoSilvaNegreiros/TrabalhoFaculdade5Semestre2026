namespace WebApplication1.Models
{
    public class PedidoItem
    {
        public int Id { get; set; }
        public int PedidoId { get; set; }
        public Pedido? Pedido { get; set; }
        public int ProdutoId { get; set; }
        public Produto? Produto { get; set; }
        public int LojistaId { get; set; }
        public Lojista? Lojista { get; set; }
        public string NomeProduto { get; set; } = string.Empty;
        public string NomeLojista { get; set; } = string.Empty;
        public int Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; }
        public decimal PercentualComissao { get; set; }
        public decimal ValorComissao { get; set; }
        public decimal ValorRepasseLojista { get; set; }
        public string CodigoRastreio { get; set; } = string.Empty;
        public string StatusEntrega { get; set; } = "Separação pelo lojista";
    }
}
