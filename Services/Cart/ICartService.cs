using WebApplication1.Models;

namespace WebApplication1.Services.Cart;

public interface ICartService
{
    Task<List<CarrinhoItem>> GetAsync(HttpContext httpContext, string usuarioEmail, CancellationToken cancellationToken);
    Task<CartOperationResult> AddAsync(HttpContext httpContext, string usuarioEmail, int produtoId, int quantidade, CancellationToken cancellationToken);
    Task<CartOperationResult> UpdateAsync(HttpContext httpContext, string usuarioEmail, int produtoId, int quantidade, CancellationToken cancellationToken);
    Task<List<CarrinhoItem>> RemoveAsync(HttpContext httpContext, string usuarioEmail, int produtoId, CancellationToken cancellationToken);
    Task SaveAsync(HttpContext httpContext, string usuarioEmail, List<CarrinhoItem> carrinho, CancellationToken cancellationToken);
    Task ClearAsync(HttpContext httpContext, string usuarioEmail, CancellationToken cancellationToken);
}
