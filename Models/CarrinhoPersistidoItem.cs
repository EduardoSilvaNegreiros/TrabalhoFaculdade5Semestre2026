namespace WebApplication1.Models;

public class CarrinhoPersistidoItem
{
    public int Id { get; set; }
    public string UsuarioEmail { get; set; } = string.Empty;
    public int ProdutoId { get; set; }
    public Produto? Produto { get; set; }
    public int Quantidade { get; set; }
    public DateTime AtualizadoEm { get; set; } = DateTime.UtcNow;
    public DateTime ExpiraEm { get; set; } = DateTime.UtcNow.AddDays(7);
}
