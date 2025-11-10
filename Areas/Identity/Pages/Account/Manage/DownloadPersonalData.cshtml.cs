// Licenciado para a .NET Foundation sob um ou mais acordos.
// A .NET Foundation licencia este arquivo sob a licença MIT.
#nullable disable

using System.Text.Json;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApplication1.Areas.Identity.Pages.Account.Manage
{
    public class DownloadPersonalDataModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<DownloadPersonalDataModel> _logger;

        public DownloadPersonalDataModel(
            UserManager<IdentityUser> userManager,
            ILogger<DownloadPersonalDataModel> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }

        public IActionResult OnGet()
        {
            // Evita acesso direto via GET (somente POST permitido)
            return NotFound();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Não foi possível carregar o usuário com o ID '{_userManager.GetUserId(User)}'.");
            }

            _logger.LogInformation("Usuário com ID '{UserId}' solicitou o download de seus dados pessoais.", _userManager.GetUserId(User));

            // Dicionário que armazenará os dados pessoais do usuário
            var dadosPessoais = new Dictionary<string, string>();

            // Obtém as propriedades marcadas com [PersonalData] em IdentityUser
            var propriedadesPessoais = typeof(IdentityUser)
                .GetProperties()
                .Where(prop => Attribute.IsDefined(prop, typeof(PersonalDataAttribute)));

            foreach (var prop in propriedadesPessoais)
            {
                dadosPessoais.Add(prop.Name, prop.GetValue(user)?.ToString() ?? "null");
            }

            // Adiciona dados de logins externos (Google, Facebook, etc)
            var logins = await _userManager.GetLoginsAsync(user);
            foreach (var login in logins)
            {
                dadosPessoais.Add($"{login.LoginProvider} - Chave de login externo", login.ProviderKey);
            }

            // Adiciona a chave do autenticador de 2FA, se existir
            dadosPessoais.Add("Chave do autenticador", await _userManager.GetAuthenticatorKeyAsync(user));

            // Configura o cabeçalho da resposta para download do arquivo
            Response.Headers.TryAdd("Content-Disposition", "attachment; filename=DadosPessoais.json");

            // Retorna o JSON contendo os dados pessoais do usuário
            return new FileContentResult(
                JsonSerializer.SerializeToUtf8Bytes(dadosPessoais),
                "application/json"
            );
        }
    }
}