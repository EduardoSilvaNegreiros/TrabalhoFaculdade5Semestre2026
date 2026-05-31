using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace WebApplication1.ViewModels;

public class ProdutoFormViewModel
{
    public int? Id { get; set; }

    public string? ImagemUrlAtual { get; set; }

    [Required(ErrorMessage = "Informe o nome do produto.")]
    [StringLength(160)]
    public string Nome { get; set; } = string.Empty;

    [Required(ErrorMessage = "Informe a descrição.")]
    [StringLength(1200)]
    public string Descricao { get; set; } = string.Empty;

    [Required(ErrorMessage = "Informe a categoria.")]
    public string Categoria { get; set; } = string.Empty;

    [Required(ErrorMessage = "Informe a marca.")]
    [StringLength(100)]
    public string Marca { get; set; } = string.Empty;

    [Range(0.01, 99999, ErrorMessage = "Informe um preço válido.")]
    public decimal Preco { get; set; }

    [Range(0, 99999, ErrorMessage = "Informe um estoque válido.")]
    public int Estoque { get; set; }

    [Required(ErrorMessage = "Informe o tipo de pele.")]
    public string TipoPele { get; set; } = "Todos";

    [Required(ErrorMessage = "Informe o tipo de cabelo.")]
    public string TipoCabelo { get; set; } = "Todos";

    public string CurvaturaCachos { get; set; } = "Todos";

    [Required(ErrorMessage = "Informe o tom.")]
    public string Tom { get; set; } = "Universal";

    [Required(ErrorMessage = "Informe o acabamento.")]
    public string Acabamento { get; set; } = "Natural";

    public bool Vegano { get; set; }

    [Required(ErrorMessage = "Informe a composição.")]
    [StringLength(1200)]
    public string Composicao { get; set; } = string.Empty;

    public IFormFile? Imagem { get; set; }
}
