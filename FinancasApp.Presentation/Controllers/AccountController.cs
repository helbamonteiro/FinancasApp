using FinancasApp.Domain.Entities;
using FinancasApp.Domain.Interfaces.Services;
using FinancasApp.Presentation.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Newtonsoft.Json;
using FinancasApp.Domain.Interface.Services;

namespace FinancasApp.Presentation.Controllers
{
    public class AccountController : Controller
    {
        //atributo
        private readonly IUsuarioDomainService? _usuarioDomainService;

        //construtor para injeção de dependência
        public AccountController(IUsuarioDomainService? usuarioDomainService)
        {
            _usuarioDomainService = usuarioDomainService;
        }

        //GET: /Account/Login
        public IActionResult Login()
        {
            return View();
        }

        //POST: /Account/Login
        [HttpPost]
        public IActionResult Login(AccountLoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var usuario = _usuarioDomainService?.Autenticar(model.Email, model.Senha);

                    var settings = new JsonSerializerSettings
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    };

                    //Criar o acesso do usuário
                    var identity = new ClaimsIdentity(new[] {
                        new Claim(ClaimTypes.Name, JsonConvert.SerializeObject(usuario, settings))
                    }, CookieAuthenticationDefaults.AuthenticationScheme);

                    //gravando o arquivo de cookie
                    var principal = new ClaimsPrincipal(identity);
                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                    //redirecionar para /Home/Index
                    return RedirectToAction("Index", "Home");
                }
                catch (Exception e)
                {
                    TempData["MensagemErro"] = e.Message;
                }
            }

            return View();
        }

        //GET: /Account/Register
        public IActionResult Register()
        {
            return View();
        }

        //POST: /Account/Register
        [HttpPost]
        public IActionResult Register(AccountRegisterViewModel model)
        {
            //verificar se todos os campos passaram nas validações
            if (ModelState.IsValid)
            {
                try
                {
                    var usuario = new Usuario
                    {
                        Id = Guid.NewGuid(),
                        Nome = model.Nome,
                        Email = model.Email,
                        Senha = model.Senha
                    };

                    _usuarioDomainService?.CriarConta(usuario);

                    TempData["MensagemSucesso"] = "Parabéns, sua conta foi criada com sucesso!";
                    ModelState.Clear(); //limpar os campos do formulário
                }
                catch (Exception e)
                {
                    TempData["MensagemErro"] = e.Message;
                }
            }

            return View();
        }

        //GET: /Account/ForgotPassword
        public IActionResult ForgotPassword()
        {
            return View();
        }

        //POST: /Account/ForgotPassword
        [HttpPost]
        public IActionResult ForgotPassword(AccountForgotPasswordViewModel model)
        {
            return View();
        }

        //GET: /Account/Logout
        public IActionResult Logout()
        {
            //apagar o cookie de autenticação do usuário.
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            //apagar os dados gravados em sessão
            HttpContext.Session.Clear();

            //redirecionar para a página inicial
            return RedirectToAction("Login", "Account");
        }
    }
}
