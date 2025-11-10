using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class ProdutoController : Controller
    {
        private static List<Produto> produtos;

        static ProdutoController()
        {
            var caminhoImagens = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "produtos");
            var arquivos = Directory.GetFiles(caminhoImagens);

            var random = new Random();

            // Dicionário com nomes e descrições específicas
            var infoProdutos = new Dictionary<string, (string Nome, string Descricao, string Categoria)>
            {
                { "TonicoFortalecimentoDeCabelo", ("Tônico Fortalecimento de Cabelo", "Tônico capilar que fortalece os fios e auxilia no crescimento saudável.", "Cabelos") },
                { "AguaMicelarNivea", ("Água Micelar Nivea", "Remove impurezas e maquiagem sem agredir a pele, deixando-a limpa e suave.", "Cuidados com a Pele") },
                { "BlushBeuty", ("Blush Beuty", "Blush rosado para todos os tons de pele, acabamento natural.", "Maquiagem") },
                { "BlushMelu", ("Blush Melu", "Blush compacto de alta pigmentação, fácil de aplicar.", "Maquiagem") },
                { "BocaRosaStick", ("Boca Rosa Stick", "Batom cremoso Boca Rosa, longa duração e cores vibrantes.", "Maquiagem") },
                { "ComboSiageHaitPlastia", ("Combo Siage Hair Plastia", "Kit de tratamento capilar que nutre, repara e devolve brilho aos fios.", "Cabelos") },
                { "CorretivoLiquidoLoreal", ("Corretivo Líquido L'Oréal", "Corretivo de alta cobertura, ideal para camuflar imperfeições.", "Maquiagem") },
                { "CremeDePentearWidiCare", ("Creme de Pentear Widi Care", "Hidrata, define e controla o frizz, facilitando o penteado de todos os tipos de cabelo.", "Cabelos") },
                { "DemaquilanteFacialNivea", ("Demaquilante Facial Nivea", "Remove maquiagem, incluindo à prova d'água, sem ressecar a pele.", "Cuidados com a Pele") },
                { "FixadorDeMaquiagem", ("Fixador de Maquiagem", "Spray fixador para maquiagem duradoura, mantém a pele fresca.", "Maquiagem") },
                { "GelDeLimpezaCeraVe", ("Gel de Limpeza CeraVe", "Gel de limpeza suave que remove impurezas sem irritar a pele.", "Cuidados com a Pele") },
                { "GlossNinaSecret", ("Gloss Nina Secret", "Gloss labial que proporciona brilho intenso e hidratação duradoura.", "Maquiagem") },
                { "HidratanteCorporalCeraVe", ("Hidratante Corporal CeraVe", "Hidrata profundamente e restaura a barreira natural da pele.", "Cuidados com a Pele") },
                { "HidratanteDermaRestauradorBenpantol", ("Hidratante Derma Restaurador Benpantol", "Creme regenerador que auxilia na recuperação da pele ressecada.", "Cuidados com a Pele") },
                { "HidratanteFacialEmpresaNeutrogena", ("Hidratante Facial Neutrogena", "Hidratação leve e de rápida absorção, indicado para uso diário.", "Cuidados com a Pele") },
                { "HidratanteLabialEmpresaNivea", ("Hidratante Labial Nivea", "Hidratante para lábios que previne ressecamento e deixa a boca macia.", "Cuidados com a Pele") },
                { "KitFranLoveLabial", ("Kit Fran Love Labial", "Kit com batons de cores variadas, ideal para presentear ou testar novas tonalidades.", "Maquiagem") },
                { "KitMaquiagem", ("Kit Maquiagem", "Conjunto completo de maquiagem para rosto, olhos e lábios.", "Maquiagem") },
                { "KitTratamentoCapilarLoreal", ("Kit Tratamento Capilar L'Oréal", "Kit completo para nutrição e reparação dos fios.", "Cabelos") },
                { "KnutKitCabeloPerfeito", ("Knut Kit Cabelo Perfeito", "Kit de cuidados capilares para manter cabelos fortes e hidratados.", "Cabelos") },
                { "MascaraCiliosEudora", ("Máscara de Cílios Eudora", "Máscara que alonga e dá volume aos cílios.", "Maquiagem") },
                { "MascaraCiliosFrancini", ("Máscara de Cílios Francini", "Máscara para cílios alongadora, volume intenso e definição.", "Maquiagem") },
                { "MascaraDeTratamentoLola", ("Máscara de Tratamento Lola", "Máscara capilar nutritiva que repara e hidrata profundamente os fios.", "Cabelos") },
                { "OceanePaletaDeSombras", ("Oceane Paleta de Sombras", "Paleta com cores variadas para criar diferentes looks de maquiagem.", "Maquiagem") },
                { "OleoDeBanhoNivea", ("Óleo de Banho Nivea", "Óleo hidratante que deixa a pele macia e nutrida.", "Cuidados com a Pele") },
                { "PoDeBananaMaquiagem", ("Pó de Banana Maquiagem", "Pó solto ideal para peles claras, acabamento matte.", "Maquiagem") },
                { "ProtetorSolarFacialPrincipia", ("Protetor Solar Facial Principia", "Protege a pele dos raios UV, prevenindo manchas e envelhecimento precoce.", "Cuidados com a Pele") },
                { "SerumDove", ("Serum Dove", "Sérum facial nutritivo que hidrata profundamente e promove viço à pele.", "Cuidados com a Pele") },
                { "SerumPrincipia", ("Serum Principia", "Sérum facial leve, indicado para hidratação e revitalização da pele.", "Cuidados com a Pele") },
                { "ShampooPantene", ("Shampoo Pantene", "Shampoo nutritivo que limpa e fortalece os fios, deixando-os macios e brilhantes.", "Cabelos") }
            };

            produtos = arquivos.Select((arquivo, index) =>
            {
                var nomeArquivo = Path.GetFileNameWithoutExtension(arquivo);

                if (infoProdutos.TryGetValue(nomeArquivo, out var info))
                {
                    return new Produto
                    {
                        Id = index + 1,
                        Nome = info.Nome,
                        Descricao = info.Descricao,
                        Preco = random.Next(30, 200),
                        ImagemUrl = $"/images/produtos/{Path.GetFileName(arquivo)}",
                        Categoria = info.Categoria
                    };
                }

                // Caso não tenha no dicionário, usa descrição genérica
                var nomeFormatado = FormatarNome(nomeArquivo);
                var categoria = DefinirCategoriaPorNome(nomeArquivo);

                return new Produto
                {
                    Id = index + 1,
                    Nome = nomeFormatado,
                    Descricao = $"Produto da categoria {categoria}: {nomeFormatado}",
                    Preco = random.Next(30, 200),
                    ImagemUrl = $"/images/produtos/{Path.GetFileName(arquivo)}",
                    Categoria = categoria
                };
            }).ToList();
        }

        private static string FormatarNome(string nomeArquivo)
        {
            var nome = Path.GetFileNameWithoutExtension(nomeArquivo)
                .Replace("-", " ")
                .Replace("_", " ")
                .Replace(".", " ");
            return System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(nome.ToLower());
        }

        private static string DefinirCategoriaPorNome(string nome)
        {
            nome = nome.ToLower();

            if (nome.Contains("shampoo") || nome.Contains("cabelo") || nome.Contains("mascara") || nome.Contains("tonico") || nome.Contains("oleo"))
                return "Cabelos";

            if (nome.Contains("hidratante") || nome.Contains("serum") || nome.Contains("agua") || nome.Contains("protetor") || nome.Contains("gel") || nome.Contains("creme"))
                return "Cuidados com a Pele";

            if (nome.Contains("blush") || nome.Contains("batom") || nome.Contains("corretivo") || nome.Contains("fixador") || nome.Contains("gloss") || nome.Contains("po") || nome.Contains("paleta"))
                return "Maquiagem";

            return "Beleza";
        }

        public IActionResult Index()
        {
            return View(produtos);
        }

        public static Produto ObterProdutoPorId(int id)
        {
            return produtos.FirstOrDefault(p => p.Id == id);
        }
    }
}