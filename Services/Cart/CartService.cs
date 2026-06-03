using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Extensions;
using WebApplication1.Models;

namespace WebApplication1.Services.Cart;

public sealed class CartService : ICartService
{
    private const int ExpirationDays = 7;
    private readonly ApplicationDbContext _context;

    public CartService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<CarrinhoItem>> GetAsync(HttpContext httpContext, string usuarioEmail, CancellationToken cancellationToken)
    {
        await RemoveExpiredItemsAsync(usuarioEmail, cancellationToken);

        var persisted = await LoadPersistedCartAsync(usuarioEmail, cancellationToken);
        if (persisted.Any())
        {
            await SaveSessionOnlyAsync(httpContext, usuarioEmail, persisted);
            return persisted;
        }

        var sessionCart = GetSessionCart(httpContext, usuarioEmail);
        if (!sessionCart.Any())
        {
            return sessionCart;
        }

        var refreshed = await RefreshCartAsync(sessionCart, cancellationToken);
        await SaveAsync(httpContext, usuarioEmail, refreshed, cancellationToken);
        return refreshed;
    }

    public async Task<CartOperationResult> AddAsync(HttpContext httpContext, string usuarioEmail, int produtoId, int quantidade, CancellationToken cancellationToken)
    {
        var produto = await GetAvailableProductAsync(produtoId, cancellationToken);
        if (produto == null)
        {
            return new CartOperationResult(false, "Produto não encontrado ou aguardando aprovação.", await GetAsync(httpContext, usuarioEmail, cancellationToken), StatusCodes.Status404NotFound);
        }

        if (produto.Estoque <= 0)
        {
            return new CartOperationResult(false, "Produto indisponível no momento.", await GetAsync(httpContext, usuarioEmail, cancellationToken), StatusCodes.Status400BadRequest);
        }

        var carrinho = await GetAsync(httpContext, usuarioEmail, cancellationToken);
        var item = carrinho.FirstOrDefault(i => i.ProdutoId == produto.Id);
        var quantidadeNormalizada = Math.Max(1, quantidade);

        if (item == null)
        {
            carrinho.Add(await CreateCartItemAsync(produto, Math.Min(quantidadeNormalizada, produto.Estoque), cancellationToken));
        }
        else
        {
            if (item.Quantidade >= produto.Estoque)
            {
                return new CartOperationResult(false, "Quantidade máxima disponível em estoque já está no carrinho.", carrinho, StatusCodes.Status400BadRequest);
            }

            item.Quantidade = Math.Min(item.Quantidade + quantidadeNormalizada, produto.Estoque);
        }

        await SaveAsync(httpContext, usuarioEmail, carrinho, cancellationToken);
        return new CartOperationResult(true, "Produto adicionado ao carrinho.", carrinho);
    }

    public async Task<CartOperationResult> UpdateAsync(HttpContext httpContext, string usuarioEmail, int produtoId, int quantidade, CancellationToken cancellationToken)
    {
        var carrinho = await GetAsync(httpContext, usuarioEmail, cancellationToken);
        var item = carrinho.FirstOrDefault(i => i.ProdutoId == produtoId);
        if (item == null)
        {
            return new CartOperationResult(false, "Item não encontrado no carrinho.", carrinho, StatusCodes.Status404NotFound);
        }

        if (quantidade <= 0)
        {
            carrinho.Remove(item);
        }
        else
        {
            var estoque = await _context.Produtos
                .Where(p => p.Id == produtoId && p.StatusModeracao == ProdutoStatusModeracao.Aprovado)
                .Select(p => p.Estoque)
                .FirstOrDefaultAsync(cancellationToken);

            item.Quantidade = Math.Min(quantidade, estoque);
            if (item.Quantidade <= 0)
            {
                carrinho.Remove(item);
            }
        }

        await SaveAsync(httpContext, usuarioEmail, carrinho, cancellationToken);
        return new CartOperationResult(true, "Carrinho atualizado.", carrinho);
    }

    public async Task<List<CarrinhoItem>> RemoveAsync(HttpContext httpContext, string usuarioEmail, int produtoId, CancellationToken cancellationToken)
    {
        var carrinho = await GetAsync(httpContext, usuarioEmail, cancellationToken);
        carrinho.RemoveAll(i => i.ProdutoId == produtoId);
        await SaveAsync(httpContext, usuarioEmail, carrinho, cancellationToken);
        return carrinho;
    }

    public async Task SaveAsync(HttpContext httpContext, string usuarioEmail, List<CarrinhoItem> carrinho, CancellationToken cancellationToken)
    {
        await SaveSessionOnlyAsync(httpContext, usuarioEmail, carrinho);
        await PersistAsync(usuarioEmail, carrinho, cancellationToken);
    }

    public async Task ClearAsync(HttpContext httpContext, string usuarioEmail, CancellationToken cancellationToken)
    {
        await SaveSessionOnlyAsync(httpContext, usuarioEmail, new List<CarrinhoItem>());
        var existentes = await _context.CarrinhoPersistidoItens
            .Where(i => i.UsuarioEmail == usuarioEmail)
            .ToListAsync(cancellationToken);

        _context.CarrinhoPersistidoItens.RemoveRange(existentes);
        await _context.SaveChangesAsync(cancellationToken);
    }

    private async Task<Produto?> GetAvailableProductAsync(int produtoId, CancellationToken cancellationToken)
    {
        return await _context.Produtos
            .Include(p => p.Lojista)
            .FirstOrDefaultAsync(p => p.Id == produtoId && p.StatusModeracao == ProdutoStatusModeracao.Aprovado, cancellationToken);
    }

    private async Task<List<CarrinhoItem>> LoadPersistedCartAsync(string usuarioEmail, CancellationToken cancellationToken)
    {
        var persistedItems = await _context.CarrinhoPersistidoItens
            .Include(i => i.Produto)
            .ThenInclude(p => p!.Lojista)
            .Where(i => i.UsuarioEmail == usuarioEmail && i.ExpiraEm > DateTime.UtcNow)
            .OrderBy(i => i.Id)
            .ToListAsync(cancellationToken);

        var cart = new List<CarrinhoItem>();
        foreach (var persisted in persistedItems)
        {
            if (persisted.Produto == null ||
                persisted.Produto.Estoque <= 0 ||
                persisted.Produto.StatusModeracao != ProdutoStatusModeracao.Aprovado)
            {
                continue;
            }

            cart.Add(await CreateCartItemAsync(
                persisted.Produto,
                Math.Min(persisted.Quantidade, persisted.Produto.Estoque),
                cancellationToken));
        }

        if (cart.Count != persistedItems.Count)
        {
            await PersistAsync(usuarioEmail, cart, cancellationToken);
        }

        return cart;
    }

    private async Task<List<CarrinhoItem>> RefreshCartAsync(List<CarrinhoItem> carrinho, CancellationToken cancellationToken)
    {
        var ids = carrinho.Select(i => i.ProdutoId).Distinct().ToList();
        var produtos = await _context.Produtos
            .Include(p => p.Lojista)
            .Where(p => ids.Contains(p.Id) && p.StatusModeracao == ProdutoStatusModeracao.Aprovado && p.Estoque > 0)
            .ToDictionaryAsync(p => p.Id, cancellationToken);

        var refreshed = new List<CarrinhoItem>();
        foreach (var item in carrinho)
        {
            if (!produtos.TryGetValue(item.ProdutoId, out var produto))
            {
                continue;
            }

            refreshed.Add(await CreateCartItemAsync(
                produto,
                Math.Min(item.Quantidade, produto.Estoque),
                cancellationToken));
        }

        return refreshed;
    }

    private async Task<CarrinhoItem> CreateCartItemAsync(Produto produto, int quantidade, CancellationToken cancellationToken)
    {
        var percentualComissao = await _context.ComissoesCategoria
            .Where(c => c.Categoria == produto.Categoria)
            .Select(c => c.Percentual)
            .FirstOrDefaultAsync(cancellationToken);

        return new CarrinhoItem
        {
            ProdutoId = produto.Id,
            Nome = produto.Nome,
            Descricao = produto.Descricao,
            ImagemUrl = produto.ImagemUrl,
            Categoria = produto.Categoria,
            Preco = produto.Preco,
            Quantidade = Math.Max(1, quantidade),
            LojistaId = produto.LojistaId,
            NomeLojista = produto.Lojista?.NomeFantasia ?? "Lojista parceiro",
            PercentualComissao = percentualComissao == 0 ? 12m : percentualComissao
        };
    }

    private async Task PersistAsync(string usuarioEmail, List<CarrinhoItem> carrinho, CancellationToken cancellationToken)
    {
        var existentes = await _context.CarrinhoPersistidoItens
            .Where(i => i.UsuarioEmail == usuarioEmail)
            .ToListAsync(cancellationToken);

        _context.CarrinhoPersistidoItens.RemoveRange(existentes);

        var now = DateTime.UtcNow;
        _context.CarrinhoPersistidoItens.AddRange(carrinho
            .Where(i => i.Quantidade > 0)
            .Select(i => new CarrinhoPersistidoItem
            {
                UsuarioEmail = usuarioEmail,
                ProdutoId = i.ProdutoId,
                Quantidade = i.Quantidade,
                AtualizadoEm = now,
                ExpiraEm = now.AddDays(ExpirationDays)
            }));

        await _context.SaveChangesAsync(cancellationToken);
    }

    private async Task RemoveExpiredItemsAsync(string usuarioEmail, CancellationToken cancellationToken)
    {
        var expirados = await _context.CarrinhoPersistidoItens
            .Where(i => i.UsuarioEmail == usuarioEmail && i.ExpiraEm <= DateTime.UtcNow)
            .ToListAsync(cancellationToken);

        if (!expirados.Any())
        {
            return;
        }

        _context.CarrinhoPersistidoItens.RemoveRange(expirados);
        await _context.SaveChangesAsync(cancellationToken);
    }

    private static List<CarrinhoItem> GetSessionCart(HttpContext httpContext, string usuarioEmail)
    {
        return httpContext.Session.GetObjectFromJson<List<CarrinhoItem>>($"Carrinho_{usuarioEmail}") ?? new List<CarrinhoItem>();
    }

    private static Task SaveSessionOnlyAsync(HttpContext httpContext, string usuarioEmail, List<CarrinhoItem> carrinho)
    {
        httpContext.Session.SetObjectAsJson($"Carrinho_{usuarioEmail}", carrinho);
        return Task.CompletedTask;
    }
}
