using System.ComponentModel.DataAnnotations;

namespace CategoriasMvc.Models;

public class ProdutoViewModel
{
    public int ProdutoId { get; set; }

    [Required(ErrorMessage = "O nome do produto e obrigatorio")]
    public string? Nome { get; set; }
    
    [Required(ErrorMessage = "A descrição do produto e obrigatorio")]
    public string? Descricao { get; set; }
    
    [Required(ErrorMessage = "Informe o preço do produto")]
    public decimal Preco { get; set; }

    [Display(Name = "Caminho da imagen")]
    public string? ImagemUrl { get; set; }

    [Display(Name = "Categoria")]
    public int CategoriaId { get; set; }
}