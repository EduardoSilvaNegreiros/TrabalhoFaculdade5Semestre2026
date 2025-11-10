using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Extensions;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class CarrinhoController : Controller
    {
        // Adiciona um produto ao carrinho do usuário logado
        [HttpPost("Carrinho/Adicionar/{id}")]
        public IActionResult Adicionar(int id)
        {
            if (!User.Identity.IsAuthenticated)
                return Unauthorized(); // Se não estiver logado, AJAX vai capturar 401

            string email = User.FindFirstValue(ClaimTypes.Email);

            var carrinho = HttpContext.Session.GetObjectFromJson<List<Produto>>($"Carrinho_{email}") ?? new List<Produto>();
            var produto = ProdutoController.ObterProdutoPorId(id);
            if (produto == null) return NotFound();

            var itemExistente = carrinho.FirstOrDefault(p => p.Id == id);
            if (itemExistente != null)
                itemExistente.Quantidade++;
            else
            {
                produto.Quantidade = 1;
                carrinho.Add(produto);
            }

            HttpContext.Session.SetObjectAsJson($"Carrinho_{email}", carrinho);

            return Json(new { sucesso = true, quantidade = carrinho.Sum(p => p.Quantidade) });
        }

        // Página do carrinho
        public IActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("AvisoLogin"); // Redireciona para aviso

            string email = User.FindFirstValue(ClaimTypes.Email);
            var carrinho = HttpContext.Session.GetObjectFromJson<List<Produto>>($"Carrinho_{email}") ?? new List<Produto>();

            return View(carrinho);
        }

        // Remover produto do carrinho
        [HttpPost("Carrinho/Remover/{id}")]
        public IActionResult Remover(int id)
        {
            if (!User.Identity.IsAuthenticated)
                return Unauthorized();

            string email = User.FindFirstValue(ClaimTypes.Email);
            var carrinho = HttpContext.Session.GetObjectFromJson<List<Produto>>($"Carrinho_{email}") ?? new List<Produto>();

            var item = carrinho.FirstOrDefault(p => p.Id == id);
            if (item != null)
            {
                carrinho.Remove(item);
                HttpContext.Session.SetObjectAsJson($"Carrinho_{email}", carrinho);
            }

            return RedirectToAction("Index");
        }

        // Obter quantidade para exibir no emoji
        [HttpGet]
        public IActionResult ObterQuantidade()
        {
            if (!User.Identity.IsAuthenticated)
                return Json(new { quantidade = 0 });

            string email = User.FindFirstValue(ClaimTypes.Email);
            var carrinho = HttpContext.Session.GetObjectFromJson<List<Produto>>($"Carrinho_{email}") ?? new List<Produto>();

            return Json(new { quantidade = carrinho.Sum(p => p.Quantidade) });
        }

        public IActionResult AvisoLogin()
        {
            return View(); // Vai buscar Views/Carrinho/AvisoLogin.cshtml
        }

        public IActionResult EscolherPagamento()
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("AvisoLogin"); // se não estiver logado

            return View(); // Vai para Views/Carrinho/EscolherPagamento.cshtml
        }

        [HttpPost]
        public IActionResult ConfirmarCompra([FromBody] dynamic data)
        {
            // Aqui só para simulação de sucesso
            return Json(new { sucesso = true });
        }

        [HttpPost]
        public IActionResult FinalizarCompra(string metodoPagamento)
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("AvisoLogin");

            string email = User.FindFirstValue(ClaimTypes.Email);

            // Pegamos o carrinho atual antes de limpar
            var carrinho = HttpContext.Session.GetObjectFromJson<List<Produto>>($"Carrinho_{email}") ?? new List<Produto>();

            // Aqui você poderia guardar a compra em DB, mas como é fictício só vamos limpar
            HttpContext.Session.SetObjectAsJson($"Carrinho_{email}", new List<Produto>());

            // Passa os dados do carrinho para a página de sucesso (opcional)
            TempData["CarrinhoResumo"] = Newtonsoft.Json.JsonConvert.SerializeObject(carrinho);

            return RedirectToAction("CompraConcluida");
        }

        public IActionResult CompraConcluida()
        {
            return View();
        }
    }
}