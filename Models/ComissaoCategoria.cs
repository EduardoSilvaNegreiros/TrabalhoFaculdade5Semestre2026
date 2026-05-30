namespace WebApplication1.Models
{
    public class ComissaoCategoria
    {
        public int Id { get; set; }
        public string Categoria { get; set; } = string.Empty;
        public decimal Percentual { get; set; }
    }
}
