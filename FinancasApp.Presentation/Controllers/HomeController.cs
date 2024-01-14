using FinancasApp.Domain.Entities;
using FinancasApp.Domain.Interfaces.Services;
using FinancasApp.Presentation.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Linq.Expressions;

namespace FinancasApp.Presentation.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IMovimentacaoDomainService? _movimentacaoDomainService;

        public HomeController(IMovimentacaoDomainService? movimentacaoDomainService)
        {
            _movimentacaoDomainService = movimentacaoDomainService;
        }

        //GET: /Home/Index
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Método para consultar os dados da página do dashboard
        /// </summary>
        public JsonResult ObterDashboard(DateTime dataInicio, DateTime dataFim)
        {
            try
            {
                //capturando o usuário da sessão
                var usuario = JsonConvert.DeserializeObject<Usuario>(User.Identity.Name);
                //consultar as movimentações do usuário no período de datas
                var movimentacoes = _movimentacaoDomainService?.Consultar(dataInicio, dataFim, usuario.Id);

                //retornando os dados para a página
                return Json(new
                {
                    totalPorTipo = ObterTotalPorTipo(movimentacoes),
                    totalPorCategoria = ObterTotalPorCategoria(movimentacoes)
                });
            }
            catch (Exception e)
            {
                return Json(e.Message);
            }
        }

        /// <summary>
        /// Método para retornar o total de contas a pagar e a receber
        /// </summary>
        private List<ChartViewModel> ObterTotalPorTipo(List<Movimentacao> movimentacoes)
        {
            var result = new List<ChartViewModel>();

            result.Add(new ChartViewModel
            {
                Name = "Total de contas a receber",
                Data = movimentacoes.Where(m => m.Tipo == TipoMovimentacao.Receita).Sum(m => m.Valor)
            });

            result.Add(new ChartViewModel
            {
                Name = "Total de contas a pagar",
                Data = movimentacoes.Where(m => m.Tipo == TipoMovimentacao.Despesa).Sum(m => m.Valor)
            });

            return result;
        }

        /// <summary>
        /// Método para retornar o total de contas por categoria
        /// </summary>
        private List<ChartViewModel> ObterTotalPorCategoria(List<Movimentacao> movimentacoes)
        {
            return movimentacoes
                        .GroupBy(g => g.Categoria?.Nome)
                        .Select(g => new ChartViewModel
                        {
                            Name = g.Key,
                            Data = g.Sum(m => m.Valor)
                        }).ToList();
        }
    }
}
