using System.ComponentModel.DataAnnotations;

namespace FinancasApp.Presentation.Models
{
    /// <summary>
    /// Modelo de dados da página /Account/ForgotPassword
    /// </summary>
    public class AccountForgotPasswordViewModel
    {
        [EmailAddress(ErrorMessage = "Por favor, informe um endereço de email válido.")]
        [Required(ErrorMessage = "Por favor, informe o seu email.")]
        public string? Email { get; set; }
    }
}
