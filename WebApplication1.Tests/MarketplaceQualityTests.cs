using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using WebApplication1.Controllers;
using WebApplication1.Controllers.Api;
using WebApplication1.Data;
using WebApplication1.Dtos.Api;
using WebApplication1.Helpers;
using WebApplication1.Models;
using WebApplication1.Services.Checkout;
using WebApplication1.Services.ProductImages;
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
                Slug = "creme-cachos-vegano",
                Nome = "Creme de Cachos Vegano",
                Descricao = "Creme para hidratar e definir cachos.",
                Categoria = "Cabelos",
                Marca = "Widi Care",
                TipoPele = "Oleosa",
                TipoCabelo = "Cacheado",
                Tom = "Universal",
                Acabamento = "Definicao",
                Vegano = true,
                Composicao = "Manteigas vegetais para hidratar.",
                Preco = 79.90m,
                Estoque = 8,
                LojistaId = lojista.Id
            },
            new Produto
            {
                Slug = "base-matte-tradicional",
                Nome = "Base Matte Tradicional",
                Descricao = "Base facial de alta cobertura.",
                Categoria = "Maquiagem",
                Marca = "Beauty Kit",
                TipoPele = "Seca",
                TipoCabelo = "Liso",
                Tom = "Medio",
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
            Slug = "agua-micelar-teste",
            Nome = "Agua Micelar",
            Descricao = "Skincare para rotina diaria.",
            Categoria = "Cuidados com a Pele",
            Marca = "Nivea",
            TipoPele = "Sensivel",
            TipoCabelo = "Todos",
            Tom = "Universal",
            Acabamento = "Suave",
            Preco = 43.30m,
            Estoque = 5,
            LojistaId = lojistaA.Id
        };
        var produtoB = new Produto
        {
            Slug = "mascara-capilar-teste",
            Nome = "Mascara Capilar",
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

    [Fact]
    public void SlugHelper_gera_slug_limpo_para_produto()
    {
        Assert.Equal("protetor-solar-toque-seco-fps50", SlugHelper.Generate("Protetor Solar Toque Seco FPS50"));
        Assert.Equal("mascara-de-cilios-volume", SlugHelper.Generate("Máscara de Cílios Volume"));
    }

    [Fact]
    public void CatalogoSeed_tem_60_produtos_com_imagens_locais_unicas()
    {
        var root = EncontrarRaizRepositorio();
        var produtos = LerCatalogoSeed(root);

        Assert.Equal(60, produtos.Count);
        Assert.Equal(20, produtos.Count(p => p.Categoria == "Maquiagem"));
        Assert.Equal(20, produtos.Count(p => p.Categoria == "Cuidados com a Pele"));
        Assert.Equal(20, produtos.Count(p => p.Categoria == "Cabelos"));

        var imagens = produtos.Select(p => p.ImagemUrl).ToList();
        Assert.Equal(imagens.Count, imagens.Distinct(StringComparer.OrdinalIgnoreCase).Count());

        foreach (var produto in produtos)
        {
            Assert.Equal($"/images/produtos/reais/{produto.Slug}.jpg", produto.ImagemUrl);

            var imagePath = Path.Combine(root, "wwwroot", produto.ImagemUrl.TrimStart('/').Replace('/', Path.DirectorySeparatorChar));
            Assert.True(File.Exists(imagePath), $"Imagem local não encontrada: {produto.ImagemUrl}");

            var (width, height) = LerDimensoesJpeg(imagePath);
            Assert.True(Math.Min(width, height) >= 600, $"Imagem menor que 600px no menor lado: {produto.ImagemUrl} ({width}x{height})");
        }
    }

    [Fact]
    public void CatalogoSeed_tem_slugs_por_nome_e_sem_mojibake()
    {
        var produtos = LerCatalogoSeed(EncontrarRaizRepositorio());

        foreach (var produto in produtos)
        {
            Assert.Equal(SlugHelper.Generate(produto.Nome), produto.Slug);

            foreach (var texto in produto.Textos())
            {
                Assert.DoesNotContain("\u00C3", texto);
                Assert.DoesNotContain("\u00C2", texto);
                Assert.DoesNotContain("\uFFFD", texto);
            }
        }
    }

    [Fact]
    public void CatalogoSeed_tem_filtros_coerentes_por_categoria()
    {
        var produtos = LerCatalogoSeed(EncontrarRaizRepositorio());
        var categorias = new[] { "Maquiagem", "Cuidados com a Pele", "Cabelos" };
        var tiposPele = new[] { "Todos", "Oleosa", "Seca", "Mista", "Sensível" };
        var tiposCabelo = new[] { "Todos", "Cacheado", "Crespo", "Liso", "Ondulado", "Danificado", "Frágil" };
        var tons = new[] { "Universal", "Claro", "Médio", "Escuro", "Preto", "Marrom" };
        var acabamentos = new[] { "Natural", "Matte", "Glow", "Suave", "Hidratante", "Brilho", "Definição", "Nutritivo", "Toque seco", "Cremoso", "Volume" };

        Assert.Contains(produtos, p => p.Vegano);

        foreach (var produto in produtos)
        {
            Assert.Contains(produto.Categoria, categorias);
            Assert.Contains(produto.TipoPele, tiposPele);
            Assert.Contains(produto.TipoCabelo, tiposCabelo);
            Assert.Contains(produto.Tom, tons);
            Assert.Contains(produto.Acabamento, acabamentos);

            if (produto.Categoria is "Maquiagem" or "Cuidados com a Pele")
            {
                Assert.Equal("Todos", produto.TipoCabelo);
            }

            if (produto.Categoria == "Cabelos")
            {
                Assert.Equal("Todos", produto.TipoPele);
            }
        }
    }

    [Fact]
    public async Task ProductImageService_rejeita_extensao_invalida()
    {
        var service = new ProductImageService(new FakeWebHostEnvironment());
        var file = CreateFormFile("produto.exe", new byte[] { 1, 2, 3 });

        var result = await service.SaveAsync(file, 1, "produto-teste", CancellationToken.None);

        Assert.False(result.Success);
        Assert.Contains("JPG", result.ErrorMessage);
    }

    [Fact]
    public async Task ProductImageService_rejeita_imagem_pequena()
    {
        var service = new ProductImageService(new FakeWebHostEnvironment());
        var file = CreateFormFile("produto.png", SmallPngBytes());

        var result = await service.SaveAsync(file, 1, "produto-teste", CancellationToken.None);

        Assert.False(result.Success);
        Assert.Contains("600", result.ErrorMessage);
    }

    [Fact]
    public async Task LojistaController_nao_edita_produto_de_outro_lojista()
    {
        await using var database = await TestDatabase.CreateAsync();
        var context = database.Context;
        var lojistaLogado = await CriarLojistaAsync(context, "Glow Brasil", "lojista@beautymarket.com");
        var outroLojista = await CriarLojistaAsync(context, "Bella Derm", "vendas@belladerm.com");
        var produtoOutroLojista = new Produto
        {
            Slug = "produto-outro-lojista",
            Nome = "Produto de outro lojista",
            Descricao = "Produto que nao pertence ao usuario logado.",
            Categoria = "Maquiagem",
            Marca = "Bella Derm",
            ImagemUrl = "/images/produtos/seed/fallback-maquiagem.png",
            Preco = 39.90m,
            Estoque = 5,
            LojistaId = outroLojista.Id
        };
        context.Produtos.Add(produtoOutroLojista);
        await context.SaveChangesAsync();

        var controller = new LojistaController(context, new FakeProductImageService())
        {
            ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(
                        new ClaimsIdentity(
                            new[] { new Claim(ClaimTypes.Email, lojistaLogado.Email) },
                            "TestAuth"))
                }
            }
        };

        var result = await controller.EditarProduto(produtoOutroLojista.Id);

        Assert.IsType<NotFoundResult>(result);
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
            Cidade = "Sao Paulo",
            Estado = "SP"
        };

        context.Lojistas.Add(lojista);
        await context.SaveChangesAsync();
        return lojista;
    }

    private static IFormFile CreateFormFile(string fileName, byte[] bytes)
    {
        return new FormFile(new MemoryStream(bytes), 0, bytes.Length, "Imagem", fileName);
    }

    private static string EncontrarRaizRepositorio()
    {
        var directory = new DirectoryInfo(AppContext.BaseDirectory);
        while (directory != null)
        {
            if (File.Exists(Path.Combine(directory.FullName, "WebApplication1.sln")) &&
                File.Exists(Path.Combine(directory.FullName, "Data", "SeedCatalog", "produtos.json")))
            {
                return directory.FullName;
            }

            directory = directory.Parent;
        }

        throw new DirectoryNotFoundException("Não foi possível localizar a raiz do repositório.");
    }

    private static IReadOnlyList<CatalogSeedProduto> LerCatalogoSeed(string root)
    {
        var path = Path.Combine(root, "Data", "SeedCatalog", "produtos.json");
        using var stream = File.OpenRead(path);
        var produtos = JsonSerializer.Deserialize<List<CatalogSeedProduto>>(stream, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        return produtos ?? new List<CatalogSeedProduto>();
    }

    private static (int Width, int Height) LerDimensoesJpeg(string path)
    {
        using var stream = File.OpenRead(path);
        if (stream.ReadByte() != 0xFF || stream.ReadByte() != 0xD8)
        {
            throw new InvalidDataException($"Arquivo não parece ser JPEG: {path}");
        }

        while (stream.Position < stream.Length)
        {
            int markerPrefix;
            do
            {
                markerPrefix = stream.ReadByte();
            }
            while (markerPrefix != 0xFF && markerPrefix != -1);

            var marker = stream.ReadByte();
            while (marker == 0xFF)
            {
                marker = stream.ReadByte();
            }

            if (marker is -1 or 0xD9)
            {
                break;
            }

            var length = ReadBigEndianUInt16(stream);
            if (length < 2)
            {
                throw new InvalidDataException($"Segmento JPEG inválido: {path}");
            }

            if (marker is 0xC0 or 0xC1 or 0xC2 or 0xC3 or 0xC5 or 0xC6 or 0xC7 or 0xC9 or 0xCA or 0xCB or 0xCD or 0xCE or 0xCF)
            {
                _ = stream.ReadByte();
                var height = ReadBigEndianUInt16(stream);
                var width = ReadBigEndianUInt16(stream);
                return (width, height);
            }

            stream.Seek(length - 2, SeekOrigin.Current);
        }

        throw new InvalidDataException($"Dimensões JPEG não encontradas: {path}");
    }

    private static int ReadBigEndianUInt16(Stream stream)
    {
        var high = stream.ReadByte();
        var low = stream.ReadByte();
        if (high < 0 || low < 0)
        {
            throw new EndOfStreamException();
        }

        return (high << 8) + low;
    }

    private static byte[] SmallPngBytes()
    {
        return Convert.FromBase64String("iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAIAAACQd1PeAAAADUlEQVR4nGNgYGD4DwABBAEAghlE7wAAAABJRU5ErkJggg==");
    }

    private sealed class FakeProductImageService : IProductImageService
    {
        public Task<ProductImageSaveResult> SaveAsync(IFormFile image, int lojistaId, string productSlug, CancellationToken cancellationToken)
        {
            return Task.FromResult(new ProductImageSaveResult(true, $"/images/produtos/uploads/{lojistaId}/{productSlug}.png", null));
        }
    }

    private sealed record CatalogSeedProduto(
        string Slug,
        string Nome,
        string Descricao,
        string Categoria,
        string Marca,
        decimal Preco,
        int Estoque,
        string TipoPele,
        string TipoCabelo,
        string CurvaturaCachos,
        string Tom,
        string Acabamento,
        bool Vegano,
        string Composicao,
        string LojistaEmail,
        string ImagemUrl)
    {
        public IEnumerable<string> Textos()
        {
            yield return Slug;
            yield return Nome;
            yield return Descricao;
            yield return Categoria;
            yield return Marca;
            yield return TipoPele;
            yield return TipoCabelo;
            yield return CurvaturaCachos;
            yield return Tom;
            yield return Acabamento;
            yield return Composicao;
            yield return LojistaEmail;
            yield return ImagemUrl;
        }
    }

    private sealed class FakeWebHostEnvironment : Microsoft.AspNetCore.Hosting.IWebHostEnvironment
    {
        public string ApplicationName { get; set; } = "Tests";
        public IFileProvider WebRootFileProvider { get; set; } = new NullFileProvider();
        public string WebRootPath { get; set; } = Path.Combine(Path.GetTempPath(), "beauty-market-tests");
        public string EnvironmentName { get; set; } = "Development";
        public string ContentRootPath { get; set; } = Directory.GetCurrentDirectory();
        public IFileProvider ContentRootFileProvider { get; set; } = new NullFileProvider();
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
