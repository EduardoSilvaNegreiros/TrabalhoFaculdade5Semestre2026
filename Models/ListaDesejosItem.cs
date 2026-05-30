namespace WebApplication1.Models
{
    public class ListaDesejosItem
    {
        public int Id { get; set; }
        public string UsuarioEmail { get; set; } = string.Empty;
        public int ProdutoId { get; set; }
        public Produto? Produto { get; set; }
        public DateTime CriadoEm { get; set; } = DateTime.UtcNow;
    }
}
