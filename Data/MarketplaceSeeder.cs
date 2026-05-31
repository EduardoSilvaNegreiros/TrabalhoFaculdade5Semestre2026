using System.Text.Json;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Helpers;
using WebApplication1.Models;

namespace WebApplication1.Data;

public static class MarketplaceSeeder
{
    public static async Task SeedAsync(IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        await db.Database.MigrateAsync();
        await SeedIdentityAsync(scope.ServiceProvider);
        await SeedMarketplaceAsync(db);
    }

    private static async Task SeedIdentityAsync(IServiceProvider services)
    {
        var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        var usuarios = new[]
        {
            ("cliente@beautymarket.com", "Cliente@123", "Consumidor"),
            ("lojista@beautymarket.com", "Lojista@123", "Lojista"),
            ("admin@beautymarket.com", "Admin@123", "Administrador")
        };

        foreach (var role in usuarios.Select(u => u.Item3).Distinct())
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        foreach (var (email, senha, role) in usuarios)
        {
            var user = await userManager.FindByEmailAsync(email);

            if (user == null)
            {
                user = new IdentityUser
                {
                    UserName = email,
                    Email = email,
                    EmailConfirmed = true
                };

                await userManager.CreateAsync(user, senha);
            }

            if (!await userManager.IsInRoleAsync(user, role))
            {
                await userManager.AddToRoleAsync(user, role);
            }
        }
    }

    private static async Task SeedMarketplaceAsync(ApplicationDbContext db)
    {
        await SeedCategoriasAsync(db);
        await SeedLojistasAsync(db);
        await db.SaveChangesAsync();

        await SeedComissoesAsync(db);
        await GarantirSlugsExistentesAsync(db);
        await SeedProdutosAsync(db);
        await db.SaveChangesAsync();

        if (!await db.Avaliacoes.AnyAsync())
        {
            var produtos = await db.Produtos.Take(12).ToListAsync();
            db.Avaliacoes.AddRange(produtos.Select(p => new Avaliacao
            {
                ProdutoId = p.Id,
                UsuarioEmail = "cliente@beautymarket.com",
                Nota = 5,
                Comentario = "Produto chegou bem embalado e ajudou na minha rotina de autocuidado.",
                MidiaUrl = p.ImagemUrl,
                TipoPele = p.TipoPele,
                TipoCabelo = p.TipoCabelo
            }));

            await db.SaveChangesAsync();
        }
    }

    private static async Task SeedCategoriasAsync(ApplicationDbContext db)
    {
        var categorias = new[]
        {
            new Categoria { Nome = "Maquiagem", Descricao = "Produtos para rosto, olhos e lábios.", PercentualComissao = 14m },
            new Categoria { Nome = "Cuidados com a Pele", Descricao = "Skincare, hidratação e proteção solar.", PercentualComissao = 12m },
            new Categoria { Nome = "Cabelos", Descricao = "Tratamento, finalização e higiene capilar.", PercentualComissao = 10m }
        };

        foreach (var categoriaSeed in categorias)
        {
            var categoria = await db.Categorias.FirstOrDefaultAsync(c => c.Nome == categoriaSeed.Nome);
            if (categoria == null)
            {
                db.Categorias.Add(categoriaSeed);
                continue;
            }

            categoria.Descricao = categoriaSeed.Descricao;
            categoria.PercentualComissao = categoriaSeed.PercentualComissao;
        }
    }

    private static async Task SeedLojistasAsync(ApplicationDbContext db)
    {
        var lojistas = new[]
        {
            new Lojista { NomeFantasia = "Glow Brasil", RazaoSocial = "Glow Brasil Cosméticos LTDA", Cnpj = "12.345.678/0001-90", Email = "lojista@beautymarket.com", Status = "Aprovado", Cidade = "São Paulo", Estado = "SP" },
            new Lojista { NomeFantasia = "Bella Derm", RazaoSocial = "Bella Derm Comércio LTDA", Cnpj = "23.456.789/0001-10", Email = "vendas@belladerm.com", Status = "Aprovado", Cidade = "Curitiba", Estado = "PR" },
            new Lojista { NomeFantasia = "Cachos & Cia", RazaoSocial = "Cachos e Cia LTDA", Cnpj = "34.567.890/0001-20", Email = "loja@cachosecia.com", Status = "Aprovado", Cidade = "Salvador", Estado = "BA" }
        };

        foreach (var lojistaSeed in lojistas)
        {
            var lojista = await db.Lojistas.FirstOrDefaultAsync(l => l.Email == lojistaSeed.Email);
            if (lojista == null)
            {
                db.Lojistas.Add(lojistaSeed);
                continue;
            }

            lojista.NomeFantasia = lojistaSeed.NomeFantasia;
            lojista.RazaoSocial = lojistaSeed.RazaoSocial;
            lojista.Cnpj = lojistaSeed.Cnpj;
            lojista.Status = lojistaSeed.Status;
            lojista.Cidade = lojistaSeed.Cidade;
            lojista.Estado = lojistaSeed.Estado;
        }
    }

    private static async Task SeedComissoesAsync(ApplicationDbContext db)
    {
        var categorias = await db.Categorias.ToListAsync();
        foreach (var categoria in categorias)
        {
            var comissao = await db.ComissoesCategoria.FirstOrDefaultAsync(c => c.Categoria == categoria.Nome);
            if (comissao == null)
            {
                db.ComissoesCategoria.Add(new ComissaoCategoria
                {
                    Categoria = categoria.Nome,
                    Percentual = categoria.PercentualComissao
                });
            }
            else
            {
                comissao.Percentual = categoria.PercentualComissao;
            }
        }
    }

    private static async Task GarantirSlugsExistentesAsync(ApplicationDbContext db)
    {
        var produtosSemSlug = await db.Produtos.Where(p => p.Slug == string.Empty).ToListAsync();
        foreach (var produto in produtosSemSlug)
        {
            produto.Slug = await CriarSlugUnicoAsync(db, produto.Nome, produto.Id);
        }
    }

    private static async Task SeedProdutosAsync(ApplicationDbContext db)
    {
        var produtosSeed = await LerProdutosSeedAsync();
        if (!produtosSeed.Any())
        {
            return;
        }

        var categorias = await db.Categorias.ToDictionaryAsync(c => c.Nome);
        var lojistas = await db.Lojistas.ToDictionaryAsync(l => l.Email);
        var lojistaFallback = await db.Lojistas.OrderBy(l => l.Id).FirstAsync();

        foreach (var seed in produtosSeed)
        {
            var slug = SlugHelper.Generate(seed.Slug);
            var produto = await db.Produtos.FirstOrDefaultAsync(p => p.Slug == slug);
            var categoria = categorias.GetValueOrDefault(seed.Categoria) ?? categorias.Values.First();
            var lojista = lojistas.GetValueOrDefault(seed.LojistaEmail) ?? lojistaFallback;
            var imagemUrl = ResolveImageUrl(seed.ImagemUrl, seed.Categoria);

            if (produto == null)
            {
                produto = new Produto { Slug = slug };
                db.Produtos.Add(produto);
            }

            produto.Nome = seed.Nome;
            produto.Descricao = seed.Descricao;
            produto.Categoria = categoria.Nome;
            produto.CategoriaId = categoria.Id;
            produto.Preco = seed.Preco;
            produto.ImagemUrl = imagemUrl;
            produto.Marca = seed.Marca;
            produto.TipoPele = seed.TipoPele;
            produto.TipoCabelo = seed.TipoCabelo;
            produto.CurvaturaCachos = seed.CurvaturaCachos;
            produto.Tom = seed.Tom;
            produto.Acabamento = seed.Acabamento;
            produto.Vegano = seed.Vegano;
            produto.Composicao = seed.Composicao;
            produto.Estoque = seed.Estoque;
            produto.LojistaId = lojista.Id;
        }
    }

    private static async Task<List<ProdutoSeed>> LerProdutosSeedAsync()
    {
        var path = Path.Combine(Directory.GetCurrentDirectory(), "Data", "SeedCatalog", "produtos.json");
        if (!File.Exists(path))
        {
            return new List<ProdutoSeed>();
        }

        await using var stream = File.OpenRead(path);
        var produtos = await JsonSerializer.DeserializeAsync<List<ProdutoSeed>>(stream, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        return produtos ?? new List<ProdutoSeed>();
    }

    private static string ResolveImageUrl(string imageUrl, string categoria)
    {
        if (ImageExists(imageUrl))
        {
            return imageUrl;
        }

        var fallback = $"/images/produtos/seed/fallback-{SlugHelper.Generate(categoria)}.png";
        return ImageExists(fallback) ? fallback : "/images/produtos/seed/fallback-produto.png";
    }

    private static bool ImageExists(string imageUrl)
    {
        if (string.IsNullOrWhiteSpace(imageUrl) || !imageUrl.StartsWith('/'))
        {
            return false;
        }

        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", imageUrl.TrimStart('/').Replace('/', Path.DirectorySeparatorChar));
        return File.Exists(path);
    }

    private static async Task<string> CriarSlugUnicoAsync(ApplicationDbContext db, string nome, int? produtoId = null)
    {
        var baseSlug = SlugHelper.Generate(nome);
        var slug = baseSlug;
        var index = 2;

        while (await db.Produtos.AnyAsync(p => p.Slug == slug && (!produtoId.HasValue || p.Id != produtoId.Value)))
        {
            slug = $"{baseSlug}-{index++}";
        }

        return slug;
    }

    private sealed record ProdutoSeed(
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
        string ImagemUrl);
}
