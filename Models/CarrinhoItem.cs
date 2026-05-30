namespace WebApplication1.Models
{
    public class CarrinhoItem
    {
        public int ProdutoId { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public string ImagemUrl { get; set; } = string.Empty;
        public string Categoria { get; set; } = string.Empty;
        public decimal Preco { get; set; }
        public int Quantidade { get; set; }
        public int LojistaId { get; set; }
        public string NomeLojista { get; set; } = string.Empty;
        public decimal PercentualComissao { get; set; }
        public decimal Subtotal => Preco * Quantidade;
        public decimal ValorComissao => Subtotal * PercentualComissao / 100m;
        public decimal ValorRepasseLojista => Subtotal - ValorComissao;
    }
}
