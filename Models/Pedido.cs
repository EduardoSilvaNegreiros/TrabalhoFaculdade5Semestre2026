namespace WebApplication1.Models
{
    public class Pedido
    {
        public int Id { get; set; }
        public string UsuarioEmail { get; set; } = string.Empty;
        public DateTime CriadoEm { get; set; } = DateTime.UtcNow;
        public string MetodoPagamento { get; set; } = "Pix";
        public string CepEntrega { get; set; } = string.Empty;
        public decimal Subtotal { get; set; }
        public decimal Frete { get; set; }
        public decimal Total { get; set; }
        public string Status { get; set; } = "Pedido confirmado";

        public List<PedidoItem> Itens { get; set; } = new();
    }
}
