namespace WebApplication1.Models
{
    public class Avaliacao
    {
        public int Id { get; set; }
        public int ProdutoId { get; set; }
        public Produto? Produto { get; set; }
        public string UsuarioEmail { get; set; } = string.Empty;
        public int Nota { get; set; }
        public string Comentario { get; set; } = string.Empty;
        public string MidiaUrl { get; set; } = string.Empty;
        public string TipoPele { get; set; } = string.Empty;
        public string TipoCabelo { get; set; } = string.Empty;
        public DateTime CriadoEm { get; set; } = DateTime.UtcNow;
    }
}
