using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Extensions;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Authorize(Roles = "Consumidor")]
    public class CarrinhoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CarrinhoController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("Carrinho/Adicionar/{id}")]
        public async Task<IActionResult> Adicionar(int id)
        {
            if (!User.Identity?.IsAuthenticated ?? true)
            {
                return Unauthorized();
            }

            var produto = await _context.Produtos.Include(p => p.Lojista).FirstOrDefaultAsync(p => p.Id == id);
            if (produto == null)
            {
                return NotFound();
            }

            var email = ObterEmail();
            var carrinho = ObterCarrinho(email);
            var itemExistente = carrinho.FirstOrDefault(p => p.ProdutoId == id);

            if (itemExistente != null)
            {
                if (itemExistente.Quantidade >= produto.Estoque)
                {
                    return Json(new
                    {
                        sucesso = false,
                        mensagem = "Quantidade máxima disponível em estoque já está no carrinho.",
                        quantidade = carrinho.Sum(p => p.Quantidade)
                    });
                }

                itemExistente.Quantidade++;
            }
            else
            {
                if (produto.Estoque <= 0)
                {
                    return Json(new
                    {
                        sucesso = false,
                        mensagem = "Produto indisponível no momento.",
                        quantidade = carrinho.Sum(p => p.Quantidade)
                    });
                }

                var comissao = await _context.ComissoesCategoria
                    .Where(c => c.Categoria == produto.Categoria)
                    .Select(c => c.Percentual)
                    .FirstOrDefaultAsync();

                carrinho.Add(new CarrinhoItem
                {
                    ProdutoId = produto.Id,
                    Nome = produto.Nome,
                    Descricao = produto.Descricao,
                    ImagemUrl = produto.ImagemUrl,
                    Categoria = produto.Categoria,
                    Preco = produto.Preco,
                    Quantidade = 1,
                    LojistaId = produto.LojistaId,
                    NomeLojista = produto.Lojista?.NomeFantasia ?? "Lojista parceiro",
                    PercentualComissao = comissao == 0 ? 12m : comissao
                });
            }

            SalvarCarrinho(email, carrinho);

            return Json(new { sucesso = true, quantidade = carrinho.Sum(p => p.Quantidade) });
        }

        public IActionResult Index()
        {
            if (!User.Identity?.IsAuthenticated ?? true)
            {
                return RedirectToAction("AvisoLogin");
            }

            var carrinho = ObterCarrinho(ObterEmail());
            ViewBag.FreteEstimado = CalcularFrete(carrinho, "01001000");
            ViewBag.CarrinhoAbandonado = carrinho.Any();
            return View(carrinho);
        }

        [HttpPost("Carrinho/Atualizar/{id}")]
        public async Task<IActionResult> Atualizar(int id, int quantidade)
        {
            if (!User.Identity?.IsAuthenticated ?? true)
            {
                return Unauthorized();
            }

            var email = ObterEmail();
            var carrinho = ObterCarrinho(email);
            var item = carrinho.FirstOrDefault(p => p.ProdutoId == id);

            if (item == null)
            {
                return RedirectToAction(nameof(Index));
            }

            var estoque = await _context.Produtos
                .Where(p => p.Id == id)
                .Select(p => p.Estoque)
                .FirstOrDefaultAsync();

            if (quantidade <= 0)
            {
                carrinho.Remove(item);
            }
            else
            {
                item.Quantidade = Math.Min(quantidade, estoque);
            }

            SalvarCarrinho(email, carrinho);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost("Carrinho/Remover/{id}")]
        public IActionResult Remover(int id)
        {
            if (!User.Identity?.IsAuthenticated ?? true)
            {
                return Unauthorized();
            }

            var email = ObterEmail();
            var carrinho = ObterCarrinho(email);
            var item = carrinho.FirstOrDefault(p => p.ProdutoId == id);
            if (item != null)
            {
                carrinho.Remove(item);
                SalvarCarrinho(email, carrinho);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult ObterQuantidade()
        {
            if (!User.Identity?.IsAuthenticated ?? true)
            {
                return Json(new { quantidade = 0 });
            }

            var carrinho = ObterCarrinho(ObterEmail());
            return Json(new { quantidade = carrinho.Sum(p => p.Quantidade) });
        }

        [AllowAnonymous]
        public IActionResult AvisoLogin()
        {
            return View();
        }

        public IActionResult EscolherPagamento(string cep = "01001000")
        {
            if (!User.Identity?.IsAuthenticated ?? true)
            {
                return RedirectToAction("AvisoLogin");
            }

            var carrinho = ObterCarrinho(ObterEmail());
            ViewBag.Cep = cep;
            ViewBag.Frete = CalcularFrete(carrinho, cep);
            ViewBag.Prazo = CalcularPrazo(carrinho);
            return View(carrinho);
        }

        [HttpPost]
        public async Task<IActionResult> FinalizarCompra(string pagamento, string cep)
        {
            if (!User.Identity?.IsAuthenticated ?? true)
            {
                return RedirectToAction("AvisoLogin");
            }

            var email = ObterEmail();
            var carrinho = ObterCarrinho(email);
            if (!carrinho.Any())
            {
                return RedirectToAction("Index");
            }

            var ids = carrinho.Select(i => i.ProdutoId).ToList();
            var produtos = await _context.Produtos
                .Where(p => ids.Contains(p.Id))
                .ToDictionaryAsync(p => p.Id);

            foreach (var item in carrinho)
            {
                if (!produtos.TryGetValue(item.ProdutoId, out var produto) || produto.Estoque < item.Quantidade)
                {
                    TempData["ErroCarrinho"] = $"Estoque insuficiente para {item.Nome}.";
                    return RedirectToAction(nameof(Index));
                }
            }

            var frete = CalcularFrete(carrinho, cep);
            var pedido = new Pedido
            {
                UsuarioEmail = email,
                MetodoPagamento = string.IsNullOrWhiteSpace(pagamento) ? "Pix" : pagamento,
                CepEntrega = cep ?? "01001000",
                Subtotal = carrinho.Sum(i => i.Subtotal),
                Frete = frete,
                Total = carrinho.Sum(i => i.Subtotal) + frete,
                Status = "Pedido confirmado"
            };

            foreach (var item in carrinho)
            {
                pedido.Itens.Add(new PedidoItem
                {
                    ProdutoId = item.ProdutoId,
                    LojistaId = item.LojistaId,
                    NomeProduto = item.Nome,
                    NomeLojista = item.NomeLojista,
                    Quantidade = item.Quantidade,
                    PrecoUnitario = item.Preco,
                    PercentualComissao = item.PercentualComissao,
                    ValorComissao = item.ValorComissao,
                    ValorRepasseLojista = item.ValorRepasseLojista,
                    CodigoRastreio = $"BM{DateTime.UtcNow:yyMMdd}{item.ProdutoId:0000}",
                    StatusEntrega = "Separação pelo lojista"
                });

                produtos[item.ProdutoId].Estoque -= item.Quantidade;
            }

            _context.Pedidos.Add(pedido);
            await _context.SaveChangesAsync();

            SalvarCarrinho(email, new List<CarrinhoItem>());
            TempData["PedidoId"] = pedido.Id;

            return RedirectToAction("CompraConcluida");
        }

        public async Task<IActionResult> CompraConcluida()
        {
            var pedidoId = TempData["PedidoId"] as int?;
            if (pedidoId == null)
            {
                return View(null);
            }

            var pedido = await _context.Pedidos.Include(p => p.Itens).FirstOrDefaultAsync(p => p.Id == pedidoId);
            return View(pedido);
        }

        private string ObterEmail()
        {
            return User.FindFirstValue(ClaimTypes.Email) ?? User.Identity?.Name ?? "visitante@beautymarket.com";
        }

        private List<CarrinhoItem> ObterCarrinho(string email)
        {
            return HttpContext.Session.GetObjectFromJson<List<CarrinhoItem>>($"Carrinho_{email}") ?? new List<CarrinhoItem>();
        }

        private void SalvarCarrinho(string email, List<CarrinhoItem> carrinho)
        {
            HttpContext.Session.SetObjectAsJson($"Carrinho_{email}", carrinho);
        }

        private static decimal CalcularFrete(List<CarrinhoItem> carrinho, string? cep)
        {
            if (!carrinho.Any())
            {
                return 0m;
            }

            var lojistas = carrinho.Select(i => i.LojistaId).Distinct().Count();
            var adicionalCep = string.IsNullOrWhiteSpace(cep) ? 0m : cep.LastOrDefault() % 3;
            return 8.90m + (lojistas * 4.50m) + adicionalCep;
        }

        private static string CalcularPrazo(List<CarrinhoItem> carrinho)
        {
            var lojistas = carrinho.Select(i => i.LojistaId).Distinct().Count();
            return $"{Math.Max(2, lojistas + 2)} a {Math.Max(4, lojistas + 5)} dias úteis";
        }
    }
}
