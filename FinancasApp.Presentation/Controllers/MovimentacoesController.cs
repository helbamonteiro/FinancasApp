using FinancasApp.Domain.Entities;
using FinancasApp.Domain.Interfaces.Services;
using FinancasApp.Domain.Services;
using FinancasApp.Presentation.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace FinancasApp.Presentation.Controllers
{
    [Authorize]
    public class MovimentacoesController : Controller
    {
        private readonly ICategoriaDomainService? _categoriaDomainService;
        private readonly IMovimentacaoDomainService? _movimentacaoDomainService;

        private const string _dataMin = "DATAMIN";
        private const string _dataMax = "DATAMAX";

        public MovimentacoesController(ICategoriaDomainService? categoriaDomainService, IMovimentacaoDomainService? movimentacaoDomainService)
        {
            _categoriaDomainService = categoriaDomainService;
            _movimentacaoDomainService = movimentacaoDomainService;
        }

        public IActionResult Cadastro()
        {
            var result = new MovimentacaoCadastroViewModel
            {
                Categorias = ObterCategorias()
            };

            return View(result);
        }

        [HttpPost]
        public IActionResult Cadastro(MovimentacaoCadastroViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var movimentacao = new Movimentacao
                    {
                        Id = Guid.NewGuid(),
                        Nome = model.Nome,
                        Data = model.Data,
                        Valor = model.Valor,
                        Tipo = (TipoMovimentacao)model.Tipo,
                        Descricao = model.Descricao,
                        CategoriaId = model.CategoriaId.Value,
                        UsuarioId = UsuarioAutenticado.Id
                    };

                    _movimentacaoDomainService.Cadastrar(movimentacao);

                    TempData["MensagemSucesso"] = $"Movimentação '{movimentacao.Nome}', cadastrada com sucesso.";
                    ModelState.Clear();
                    model = new MovimentacaoCadastroViewModel();
                }
                catch (Exception e)
                {
                    TempData["MensagemErro"] = e.Message;
                }
            }

            model.Categorias = ObterCategorias();
            return View(model);
        }

        public IActionResult Consulta()
        {
            var model = new MovimentacaoConsultaViewModel();

            try
            {
                //verificando se existem datas armazenadas em sessão
                if (HttpContext.Session.GetString(_dataMin) != null
                    && HttpContext.Session.GetString(_dataMax) != null)
                {
                    model.DataMin = DateTime.Parse(HttpContext.Session.GetString(_dataMin));
                    model.DataMax = DateTime.Parse(HttpContext.Session.GetString(_dataMax));

                    //consultar as movimentações
                    model.Movimentacoes = _movimentacaoDomainService.Consultar
                        (model.DataMin.Value, model.DataMax.Value, UsuarioAutenticado.Id);
                }
            }
            catch (Exception e)
            {
                TempData["MensagemErro"] = e.Message;
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult Consulta(MovimentacaoConsultaViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //consultar as movimentações
                    model.Movimentacoes = _movimentacaoDomainService.Consultar
                        (model.DataMin.Value, model.DataMax.Value, UsuarioAutenticado.Id);

                    TempData["MensagemSucesso"] = $"Consulta realizada com sucesso: {model.Movimentacoes.Count} registro(s) obtido(s).";

                    //guardar as datas selecionadas em uma sessão
                    HttpContext.Session.SetString(_dataMin, model.DataMin.ToString());
                    HttpContext.Session.SetString(_dataMax, model.DataMax.ToString());
                }
                catch (Exception e)
                {
                    TempData["MensagemErro"] = e.Message;
                }
            }

            return View(model);
        }

        public IActionResult Edicao(Guid id)
        {
            var model = new MovimentacaoEdicaoViewModel();

            try
            {
                var movimentacao = _movimentacaoDomainService?.ObterPorId(id);

                if (movimentacao != null && movimentacao.UsuarioId == UsuarioAutenticado.Id)
                {
                    model.Id = movimentacao.Id;
                    model.Nome = movimentacao.Nome;
                    model.Data = movimentacao.Data;
                    model.Valor = movimentacao.Valor;
                    model.Tipo = (int)movimentacao.Tipo;
                    model.Descricao = movimentacao.Descricao;
                    model.CategoriaId = movimentacao.CategoriaId;
                    model.Categorias = ObterCategorias();
                }
                else
                {
                    return RedirectToAction("Consulta");
                }
            }
            catch (Exception e)
            {
                TempData["MensagemErro"] = e.Message;
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult Edicao(MovimentacaoEdicaoViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var movimentacao = new Movimentacao
                    {
                        Id = model.Id.Value,
                        Nome = model.Nome,
                        Data = model.Data.Value,
                        Valor = model.Valor.Value,
                        Tipo = (TipoMovimentacao)model.Tipo,
                        Descricao = model.Descricao,
                        CategoriaId = model.CategoriaId.Value,
                        UsuarioId = UsuarioAutenticado.Id
                    };

                    _movimentacaoDomainService.Atualizar(movimentacao);
                    TempData["MensagemSucesso"] = $"Movimentação '{movimentacao.Nome}', atualizada com sucesso.";
                }
                catch (Exception e)
                {
                    TempData["MensagemErro"] = e.Message;
                }
            }

            model.Categorias = ObterCategorias();
            return View(model);
        }

        public IActionResult Exclusao(Guid id)
        {
            try
            {
                _movimentacaoDomainService?.Excluir(id);
                TempData["MensagemSucesso"] = "Movimentação excluída com sucesso.";
            }
            catch (Exception e)
            {
                TempData["MensagemErro"] = e.Message;
            }

            return RedirectToAction("Consulta");
        }

        /// <summary>
        /// Método para retornar uma lista de itens de seleção
        /// preenchido com as categorias obtidas do banco de dados
        /// </summary>
        private List<SelectListItem> ObterCategorias()
        {
            var lista = new List<SelectListItem>();

            foreach (var item in _categoriaDomainService?.Consultar())
            {
                lista.Add(new SelectListItem
                {
                    Value = item.Id.ToString(), //valor do campo
                    Text = item.Nome, //Texto exibido no campo
                });
            }

            return lista;
        }

        /// <summary>
        /// Método para retornar os dados do usuário autenticado no AspNet
        /// </summary>
        private Usuario UsuarioAutenticado
        {
            get => JsonConvert.DeserializeObject<Usuario>(User.Identity.Name);
        }
    }
}
