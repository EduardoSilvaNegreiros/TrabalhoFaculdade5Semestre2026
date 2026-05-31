using Microsoft.AspNetCore.Authorization;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Controllers;
using WebApplication1.Controllers.Api;
using WebApplication1.Data;
using WebApplication1.Dtos.Api;
using WebApplication1.Models;
using WebApplication1.Services.Checkout;
using WebApplication1.Services.Recommendations;

namespace WebApplication1.Tests;

public sealed class MarketplaceQualityTests
{
    [Fact]
    public async Task RecommendationService_prioriza_produto_compativel_com_perfil()
    {
        await using var database = await TestDatabase.CreateAsync();
        var context = database.Context;
        var lojista = await CriarLojistaAsync(context, "Glow Brasil", "lojista@beautymarket.com");

        context.Produtos.AddRange(
            new Produto
            {
                Nome = "Creme de Cachos Vegano",
                Descricao = "Creme para hidratar e definir cachos.",
                Categoria = "Cabelos",
                Marca = "Widi Care",
                TipoPele = "Oleosa",
                TipoCabelo = "Cacheado",
                Tom = "Universal",
                Acabamento = "Definição",
                Vegano = true,
                Composicao = "Manteigas vegetais para hidratar.",
                Preco = 79.90m,
                Estoque = 8,
                LojistaId = lojista.Id
            },
            new Produto
            {
                Nome = "Base Matte Tradicional",
                Descricao = "Base facial de alta cobertura.",
                Categoria = "Maquiagem",
                Marca = "Beauty Kit",
                TipoPele = "Seca",
                TipoCabelo = "Liso",
                Tom = "Médio",
                Acabamento = "Matte",
                Vegano = false,
                Composicao = "Pigmentos minerais.",
                Preco = 49.90m,
                Estoque = 12,
                LojistaId = lojista.Id
            });
        await context.SaveChangesAsync();

        var service = new ProductRecommendationService(
            context,
            new IProductRecommendationStrategy[]
            {
                new SkinHairRecommendationStrategy(),
                new CategoryRecommendationStrategy(),
                new VeganPriceRecommendationStrategy()
            });

        var recomendacoes = await service.RecommendAsync(
            new ProductRecommendationRequest("Oleosa", "Cacheado", "Cabelos", true, 120m, "hidratar"),
            2,
            CancellationToken.None);

        Assert.Equal("Creme de Cachos Vegano", recomendacoes.First().Nome);
    }

    [Fact]
    public async Task CheckoutFacade_cria_pedido_multilojista_com_rastreio_e_baixa_estoque()
    {
        await using var database = await TestDatabase.CreateAsync();
        var context = database.Context;

        var lojistaA = await CriarLojistaAsync(context, "Glow Brasil", "lojista@beautymarket.com");
        var lojistaB = await CriarLojistaAsync(context, "Bella Derm", "vendas@belladerm.com");

        var produtoA = new Produto
        {
            Nome = "Água Micelar",
            Descricao = "Skincare para rotina diária.",
            Categoria = "Cuidados com a Pele",
            Marca = "Nivea",
            TipoPele = "Sensível",
            TipoCabelo = "Todos",
            Tom = "Universal",
            Acabamento = "Suave",
            Preco = 43.30m,
            Estoque = 5,
            LojistaId = lojistaA.Id
        };
        var produtoB = new Produto
        {
            Nome = "Máscara Capilar",
            Descricao = "Tratamento nutritivo.",
            Categoria = "Cabelos",
            Marca = "Lola",
            TipoPele = "Todos",
            TipoCabelo = "Cacheado",
            Tom = "Universal",
            Acabamento = "Nutritivo",
            Preco = 59.90m,
            Estoque = 4,
            LojistaId = lojistaB.Id
        };

        context.Produtos.AddRange(produtoA, produtoB);
        await context.SaveChangesAsync();

        var carrinho = new List<CarrinhoItem>
        {
            new()
            {
                ProdutoId = produtoA.Id,
                Nome = produtoA.Nome,
                Categoria = produtoA.Categoria,
                Preco = produtoA.Preco,
                Quantidade = 2,
                LojistaId = lojistaA.Id,
                NomeLojista = lojistaA.NomeFantasia,
                PercentualComissao = 12m
            },
            new()
            {
                ProdutoId = produtoB.Id,
                Nome = produtoB.Nome,
                Categoria = produtoB.Categoria,
                Preco = produtoB.Preco,
                Quantidade = 1,
                LojistaId = lojistaB.Id,
                NomeLojista = lojistaB.NomeFantasia,
                PercentualComissao = 10m
            }
        };

        var facade = new CheckoutFacade(context);
        var resultado = await facade.FinalizarAsync(
            "cliente@beautymarket.com",
            carrinho,
            new CheckoutRequest("Pix", "01001000"),
            CancellationToken.None);

        Assert.True(resultado.Success);
        Assert.NotNull(resultado.Pedido);
        Assert.Equal(2, resultado.Pedido.Itens.Count);
        Assert.Equal(2, resultado.Pedido.Itens.Select(i => i.LojistaId).Distinct().Count());
        Assert.All(resultado.Pedido.Itens, item => Assert.StartsWith("BM", item.CodigoRastreio));
        Assert.Equal(3, (await context.Produtos.FindAsync(produtoA.Id))!.Estoque);
        Assert.Equal(3, (await context.Produtos.FindAsync(produtoB.Id))!.Estoque);
        Assert.True(resultado.Pedido.Total > resultado.Pedido.Subtotal);
    }

    [Fact]
    public void Controllers_protegidos_exigem_roles_corretas()
    {
        AssertControllerRole<ConsumidorController>("Consumidor");
        AssertControllerRole<CarrinhoController>("Consumidor");
        AssertControllerRole<ListaDesejosController>("Consumidor");
        AssertControllerRole<PedidosController>("Consumidor");
        AssertControllerRole<ApiCarrinhoController>("Consumidor");
        AssertControllerRole<ApiCheckoutController>("Consumidor");
        AssertControllerRole<ApiPedidosController>("Consumidor");

        AssertControllerRole<LojistaController>("Lojista");
        AssertControllerRole<AdminController>("Administrador");
    }

    private static void AssertControllerRole<TController>(string expectedRole)
    {
        var attribute = typeof(TController)
            .GetCustomAttributes(typeof(AuthorizeAttribute), inherit: true)
            .Cast<AuthorizeAttribute>()
            .FirstOrDefault();

        Assert.NotNull(attribute);
        Assert.Equal(expectedRole, attribute.Roles);
    }

    private static async Task<Lojista> CriarLojistaAsync(ApplicationDbContext context, string nome, string email)
    {
        var lojista = new Lojista
        {
            NomeFantasia = nome,
            RazaoSocial = $"{nome} LTDA",
            Cnpj = Guid.NewGuid().ToString("N")[..14],
            Email = email,
            Status = "Aprovado",
            Cidade = "São Paulo",
            Estado = "SP"
        };

        context.Lojistas.Add(lojista);
        await context.SaveChangesAsync();
        return lojista;
    }

    private sealed class TestDatabase : IAsyncDisposable
    {
        private readonly SqliteConnection _connection;

        private TestDatabase(SqliteConnection connection, ApplicationDbContext context)
        {
            _connection = connection;
            Context = context;
        }

        public ApplicationDbContext Context { get; }

        public static async Task<TestDatabase> CreateAsync()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            await connection.OpenAsync();

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlite(connection)
                .Options;

            var context = new ApplicationDbContext(options);
            await context.Database.EnsureCreatedAsync();

            return new TestDatabase(connection, context);
        }

        public async ValueTask DisposeAsync()
        {
            await Context.DisposeAsync();
            await _connection.DisposeAsync();
        }
    }
}
