using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
        if (!await db.Categorias.AnyAsync())
        {
            db.Categorias.AddRange(
                new Categoria { Nome = "Maquiagem", Descricao = "Produtos para rosto, olhos e lábios.", PercentualComissao = 14m },
                new Categoria { Nome = "Cuidados com a Pele", Descricao = "Skincare, hidratação e proteção solar.", PercentualComissao = 12m },
                new Categoria { Nome = "Cabelos", Descricao = "Tratamento, finalização e higiene capilar.", PercentualComissao = 10m }
            );
        }

        if (!await db.Lojistas.AnyAsync())
        {
            db.Lojistas.AddRange(
                new Lojista { NomeFantasia = "Glow Brasil", RazaoSocial = "Glow Brasil Cosméticos LTDA", Cnpj = "12.345.678/0001-90", Email = "lojista@beautymarket.com", Status = "Aprovado", Cidade = "São Paulo", Estado = "SP" },
                new Lojista { NomeFantasia = "Bella Derm", RazaoSocial = "Bella Derm Comércio LTDA", Cnpj = "23.456.789/0001-10", Email = "vendas@belladerm.com", Status = "Aprovado", Cidade = "Curitiba", Estado = "PR" },
                new Lojista { NomeFantasia = "Cachos & Cia", RazaoSocial = "Cachos e Cia LTDA", Cnpj = "34.567.890/0001-20", Email = "loja@cachosecia.com", Status = "Pendente", Cidade = "Salvador", Estado = "BA" }
            );
        }

        await db.SaveChangesAsync();

        if (!await db.Lojistas.AnyAsync(l => l.Email == "lojista@beautymarket.com"))
        {
            var lojistaDemo = await db.Lojistas.OrderBy(l => l.Id).FirstOrDefaultAsync(l => l.Status == "Aprovado")
                ?? await db.Lojistas.OrderBy(l => l.Id).FirstOrDefaultAsync();

            if (lojistaDemo != null)
            {
                lojistaDemo.Email = "lojista@beautymarket.com";
                await db.SaveChangesAsync();
            }
        }

        if (!await db.ComissoesCategoria.AnyAsync())
        {
            var categoriasComissao = await db.Categorias.ToListAsync();
            db.ComissoesCategoria.AddRange(categoriasComissao.Select(c => new ComissaoCategoria
            {
                Categoria = c.Nome,
                Percentual = c.PercentualComissao
            }));
        }

        if (!await db.Produtos.AnyAsync())
        {
            await SeedProdutosAsync(db);
        }

        await db.SaveChangesAsync();

        if (!await db.Avaliacoes.AnyAsync())
        {
            var produtos = await db.Produtos.Take(8).ToListAsync();
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

    private static async Task SeedProdutosAsync(ApplicationDbContext db)
    {
        var categorias = await db.Categorias.ToDictionaryAsync(c => c.Nome);
        var lojistas = await db.Lojistas.OrderBy(l => l.Id).ToListAsync();
        var detalhes = ProdutosBase();
        var imagensPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "produtos");
        var extensoes = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { ".jpg", ".jpeg", ".png", ".webp" };
        var arquivos = Directory.Exists(imagensPath)
            ? Directory.GetFiles(imagensPath).Where(a => extensoes.Contains(Path.GetExtension(a))).OrderBy(a => a).ToList()
            : new List<string>();

        for (var index = 0; index < arquivos.Count; index++)
        {
            var arquivo = arquivos[index];
            var nomeArquivo = Path.GetFileNameWithoutExtension(arquivo);
            var chave = nomeArquivo.Split('.')[0];
            var produtoSeed = detalhes.TryGetValue(chave, out var produtoDetalhe)
                ? produtoDetalhe
                : CriarProdutoGenerico(chave);
            var categoria = categorias.GetValueOrDefault(produtoSeed.Categoria) ?? categorias.Values.First();
            var lojista = lojistas[index % lojistas.Count];

            db.Produtos.Add(new Produto
            {
                Nome = produtoSeed.Nome,
                Descricao = produtoSeed.Descricao,
                Categoria = categoria.Nome,
                CategoriaId = categoria.Id,
                Preco = 29.90m + (index * 7 % 120),
                ImagemUrl = $"/images/produtos/{Path.GetFileName(arquivo)}",
                Marca = produtoSeed.Marca,
                TipoPele = produtoSeed.TipoPele,
                TipoCabelo = produtoSeed.TipoCabelo,
                CurvaturaCachos = produtoSeed.Curvatura,
                Tom = produtoSeed.Tom,
                Acabamento = produtoSeed.Acabamento,
                Vegano = index % 3 == 0,
                Composicao = produtoSeed.Composicao,
                Estoque = 12 + index,
                LojistaId = lojista.Id
            });
        }
    }

    private static Dictionary<string, ProdutoSeed> ProdutosBase() => new()
    {
        ["AguaMicelarNivea"] = new("Água Micelar Nivea", "Remove impurezas e maquiagem sem agredir a pele.", "Cuidados com a Pele", "Nivea", "Sensível", "Todos", "Todos", "Universal", "Suave", "Micelas e agentes hidratantes."),
        ["BlushBeuty"] = new("Blush Beuty", "Blush rosado para todos os tons de pele.", "Maquiagem", "Beuty", "Todos", "Todos", "Todos", "Médio", "Natural", "Pigmentos minerais."),
        ["BlushMelu"] = new("Blush Melu", "Blush compacto de alta pigmentação, fácil de aplicar.", "Maquiagem", "Melu", "Todos", "Todos", "Todos", "Claro", "Matte", "Pigmentos e emolientes."),
        ["BocaRosaStick"] = new("Boca Rosa Stick", "Batom cremoso de longa duração.", "Maquiagem", "Boca Rosa", "Todos", "Todos", "Todos", "Universal", "Cremoso", "Ceras vegetais e pigmentos."),
        ["ComboSiageHaitPlastia"] = new("Combo Siage Hair Plastia", "Kit de tratamento capilar que nutre e repara.", "Cabelos", "Eudora", "Todos", "Danificado", "Todos", "Universal", "Brilho", "Proteínas e óleos nutritivos."),
        ["CorretivoLiquidoLoreal"] = new("Corretivo Líquido L'Oreal", "Corretivo de alta cobertura.", "Maquiagem", "L'Oreal", "Mista", "Todos", "Todos", "Médio", "Matte", "Pigmentos de longa duração."),
        ["CremeDePentearWidiCare"] = new("Creme de Pentear Widi Care", "Hidrata, define e controla o frizz.", "Cabelos", "Widi Care", "Todos", "Cacheado", "3A-4C", "Universal", "Definição", "Manteigas vegetais."),
        ["DemaquilanteFacialNivea"] = new("Demaquilante Facial Nivea", "Remove maquiagem sem ressecar a pele.", "Cuidados com a Pele", "Nivea", "Seca", "Todos", "Todos", "Universal", "Suave", "Agentes de limpeza e hidratação."),
        ["FixadorDeMaquiagem"] = new("Fixador de Maquiagem", "Spray fixador para maquiagem duradoura.", "Maquiagem", "Ruby Rose", "Oleosa", "Todos", "Todos", "Universal", "Natural", "Polímeros fixadores."),
        ["GelDeLimpezaCeraVe"] = new("Gel de Limpeza CeraVe", "Gel suave que remove impurezas.", "Cuidados com a Pele", "CeraVe", "Oleosa", "Todos", "Todos", "Universal", "Suave", "Ceramidas e niacinamida."),
        ["GlossNinaSecret"] = new("Gloss Nina Secret", "Gloss labial com brilho intenso.", "Maquiagem", "Nina Secrets", "Todos", "Todos", "Todos", "Universal", "Glow", "Óleos e pigmentos."),
        ["HidratanteCorporalCeraVe"] = new("Hidratante Corporal CeraVe", "Hidrata profundamente a pele.", "Cuidados com a Pele", "CeraVe", "Seca", "Todos", "Todos", "Universal", "Hidratante", "Ceramidas."),
        ["HidratanteDermaRestauradorBenpantol"] = new("Hidratante Derma Restaurador Bepantol", "Creme regenerador para pele ressecada.", "Cuidados com a Pele", "Bepantol", "Seca", "Todos", "Todos", "Universal", "Hidratante", "Dexpantenol."),
        ["HidratanteFacialEmpresaNeutrogena"] = new("Hidratante Facial Neutrogena", "Hidratação leve de rápida absorção.", "Cuidados com a Pele", "Neutrogena", "Mista", "Todos", "Todos", "Universal", "Leve", "Glicerina."),
        ["HidratanteLabialEmpresaNivea"] = new("Hidratante Labial Nivea", "Previne ressecamento dos lábios.", "Cuidados com a Pele", "Nivea", "Todos", "Todos", "Todos", "Universal", "Hidratante", "Manteiga de karité."),
        ["KitFranLoveLabial"] = new("Kit Fran Love Labial", "Kit com batons de cores variadas.", "Maquiagem", "Franciny Ehlke", "Todos", "Todos", "Todos", "Universal", "Cremoso", "Ceras e pigmentos."),
        ["KitMaquiagem"] = new("Kit Maquiagem", "Conjunto completo para rosto, olhos e lábios.", "Maquiagem", "Beauty Kit", "Todos", "Todos", "Todos", "Universal", "Variado", "Pigmentos selecionados."),
        ["KitTratamentoCapilarLoreal"] = new("Kit Tratamento Capilar L'Oreal", "Kit para nutrição e reparação.", "Cabelos", "L'Oreal", "Todos", "Danificado", "Todos", "Universal", "Brilho", "Queratina e vitaminas."),
        ["KnutKitCabeloPerfeito"] = new("Knut Kit Cabelo Perfeito", "Kit para cabelos fortes e hidratados.", "Cabelos", "Knut", "Todos", "Todos", "Todos", "Universal", "Hidratante", "Óleos nutritivos."),
        ["MascaraCiliosEudora"] = new("Máscara de Cílios Eudora", "Máscara que alonga e dá volume.", "Maquiagem", "Eudora", "Todos", "Todos", "Todos", "Preto", "Volume", "Ceras modeladoras."),
        ["MascaraCiliosFrancini"] = new("Máscara de Cílios Franciny", "Volume intenso e definição.", "Maquiagem", "Franciny Ehlke", "Todos", "Todos", "Todos", "Preto", "Volume", "Ceras modeladoras."),
        ["MascaraDeTratamentoLola"] = new("Máscara de Tratamento Lola", "Máscara capilar nutritiva.", "Cabelos", "Lola", "Todos", "Cacheado", "2A-4C", "Universal", "Nutritivo", "Manteigas e óleos."),
        ["OceanePaletaDeSombras"] = new("Océane Paleta de Sombras", "Paleta com cores variadas.", "Maquiagem", "Océane", "Todos", "Todos", "Todos", "Universal", "Cintilante", "Pigmentos prensados."),
        ["OleoDeBanhoNivea"] = new("Óleo de Banho Nivea", "Óleo hidratante para pele macia.", "Cuidados com a Pele", "Nivea", "Seca", "Todos", "Todos", "Universal", "Hidratante", "Óleos corporais."),
        ["PoDeBananaMaquiagem"] = new("Pó de Banana Maquiagem", "Pó solto com acabamento matte.", "Maquiagem", "Vizzela", "Oleosa", "Todos", "Todos", "Claro", "Matte", "Pó mineral."),
        ["ProtetorSolarFacialPrincipia"] = new("Protetor Solar Facial Principia", "Protege dos raios UV e previne manchas.", "Cuidados com a Pele", "Principia", "Oleosa", "Todos", "Todos", "Universal", "Toque seco", "Filtros solares."),
        ["SerumDove"] = new("Serum Dove", "Serum facial nutritivo.", "Cuidados com a Pele", "Dove", "Mista", "Todos", "Todos", "Universal", "Glow", "Ativos hidratantes."),
        ["SerumPrincipia"] = new("Serum Principia", "Serum leve para hidratação.", "Cuidados com a Pele", "Principia", "Mista", "Todos", "Todos", "Universal", "Leve", "Niacinamida."),
        ["ShampooPantene"] = new("Shampoo Pantene", "Shampoo nutritivo para fios fortes.", "Cabelos", "Pantene", "Todos", "Todos", "Todos", "Universal", "Brilho", "Pro-vitaminas."),
        ["TonicoFortalecimentoDeCabelo"] = new("Tônico Fortalecimento de Cabelo", "Tônico capilar para fortalecer os fios.", "Cabelos", "BioHair", "Todos", "Frágil", "2A-4C", "Universal", "Leve", "Extratos vegetais e vitaminas.")
    };

    private static ProdutoSeed CriarProdutoGenerico(string nomeArquivo)
    {
        var nome = nomeArquivo.Replace("-", " ").Replace("_", " ");
        return new ProdutoSeed(nome, $"Produto selecionado para a rotina de beleza: {nome}.", "Maquiagem", "BeautyMarket", "Todos", "Todos", "Todos", "Universal", "Natural", "Composição informativa.");
    }

    private record ProdutoSeed(string Nome, string Descricao, string Categoria, string Marca, string TipoPele, string TipoCabelo, string Curvatura, string Tom, string Acabamento, string Composicao);
}
