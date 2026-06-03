using WebApplication1.Models;

namespace WebApplication1.Services.Cart;

public sealed record CartOperationResult(
    bool Success,
    string? Message,
    List<CarrinhoItem> Items,
    int StatusCode = StatusCodes.Status200OK);
