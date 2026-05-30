namespace WebApplication1.Models
{
    public class Lojista
    {
        public int Id { get; set; }
        public string NomeFantasia { get; set; } = string.Empty;
        public string RazaoSocial { get; set; } = string.Empty;
        public string Cnpj { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Status { get; set; } = "Pendente";
        public string Cidade { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
        public DateTime CriadoEm { get; set; } = DateTime.UtcNow;

        public List<Produto> Produtos { get; set; } = new();
    }
}
