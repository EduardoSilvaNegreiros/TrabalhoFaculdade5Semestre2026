using WebApplication1.Models;

namespace WebApplication1.Services.Checkout;

public sealed record CheckoutResult(
    bool Success,
    string Message,
    Pedido? Pedido,
    decimal Frete,
    string Prazo);

