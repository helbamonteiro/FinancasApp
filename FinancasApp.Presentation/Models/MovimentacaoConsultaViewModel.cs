using FinancasApp.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace FinancasApp.Presentation.Models
{
    /// <summary>
    /// Modelo de dados para a página de consulta de movimentações
    /// </summary>
    public class MovimentacaoConsultaViewModel
    {
        [Required(ErrorMessage = "Por favor, informe a data de início.")]
        public DateTime? DataMin { get; set; }

        [Required(ErrorMessage = "Por favor, informe a data de fim.")]
        public DateTime? DataMax { get; set; }

        /// <summary>
        /// Lista para exibir o resultado da consulta de movimentações
        /// </summary>
        public List<Movimentacao>? Movimentacoes { get; set; }
    }
}
