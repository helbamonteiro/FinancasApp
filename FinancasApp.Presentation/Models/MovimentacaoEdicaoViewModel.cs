using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace FinancasApp.Presentation.Models
{
    /// <summary>
    /// Modelo de dados para a página de edição de movimentação
    /// </summary>
    public class MovimentacaoEdicaoViewModel
    {
        public Guid? Id { get; set; }

        [MinLength(8, ErrorMessage = "Informe no mínimo {1} caracteres.")]
        [MaxLength(100, ErrorMessage = "Informe no máximo {1} caracteres.")]
        [Required(ErrorMessage = "Por favor, informe o nome da movimentação.")]
        public string? Nome { get; set; }

        [Required(ErrorMessage = "Por favor, informe o data da movimentação.")]
        public DateTime? Data { get; set; }

        [Required(ErrorMessage = "Por favor, informe o valor da movimentação.")]
        public decimal? Valor { get; set; }

        [MinLength(8, ErrorMessage = "Informe no mínimo {1} caracteres.")]
        [MaxLength(250, ErrorMessage = "Informe no máximo {1} caracteres.")]
        [Required(ErrorMessage = "Por favor, informe a descrição da movimentação.")]
        public string? Descricao { get; set; }

        [Required(ErrorMessage = "Por favor, selecione o tipo da movimentação.")]
        public int? Tipo { get; set; }

        [Required(ErrorMessage = "Por favor, selecione a categoria da movimentação.")]
        public Guid? CategoriaId { get; set; }

        /// <summary>
        /// Lista de opções para o campo DropDownList da página
        /// </summary>
        public List<SelectListItem>? Categorias { get; set; }
    }
}
