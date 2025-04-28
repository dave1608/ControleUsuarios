using ControleUsuarios.Models;
using ControleUsuarios.Repositorio;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ControleUsuarios.Controllers
{
    public class LoginController : Controller
    {
    
        private readonly IUsuarioRepositorio _usuarioRepositorio;

        public LoginController(IUsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Entrar(LoginModel loginModel)
        {
            try
            {
                if (ModelState.IsValid) 
                {
                    UsuarioModel usuario = _usuarioRepositorio.BuscarPorLogin(loginModel.Login);
                    if (usuario != null )
                    {
                        if (usuario.SenhaValida(loginModel.Senha))
                        {
                        return RedirectToAction("Index", "Home");
                        }
                        
                        TempData["MensagemErro"] = $"=Senha inválida, tente novamente.";

                    }

                    TempData["MensagemErro"] = $"Usuário ou senha inválidos, tente novamente.";
                }

                return View("Index");
            }
            catch (Exception erro)
            {
                return RedirectToAction("Index");
            }
        }
    }
}
