namespace WebApplication1.Models
{
    public class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public decimal Preco { get; set; }
        public int Quantidade { get; set; }
        public string Categoria { get; set; } = "Outros";
        public string ImagemUrl { get; set; } = string.Empty;
        public string Marca { get; set; } = string.Empty;
        public string TipoPele { get; set; } = "Todos";
        public string TipoCabelo { get; set; } = "Todos";
        public string CurvaturaCachos { get; set; } = "Todos";
        public string Tom { get; set; } = "Universal";
        public string Acabamento { get; set; } = "Natural";
        public bool Vegano { get; set; }
        public string Composicao { get; set; } = string.Empty;
        public int Estoque { get; set; } = 20;

        public int? CategoriaId { get; set; }
        public Categoria? CategoriaRelacionada { get; set; }

        public int LojistaId { get; set; }
        public Lojista? Lojista { get; set; }
    }
}
