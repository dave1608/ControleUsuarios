using ControleUsuarios.Helper;
using ControleUsuarios.Models;
using ControleUsuarios.Repositorio;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ControleUsuarios.Controllers
{
    public class LoginController : Controller
    {
    
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly ISessao _sessao;

        public LoginController(IUsuarioRepositorio usuarioRepositorio, ISessao sessao)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _sessao = sessao;
        }

        public IActionResult Index()
        {
            // Se o usuário estiver logado, redirecionar para a home

            if (_sessao.BuscarSessaoDoUsuario() != null) return RedirectToAction("Index", "Home");

            return View();
        }

        public IActionResult Sair() 
        {
            _sessao.RemoverSessaoDoUsuario();

            return RedirectToAction("Index", "Login");
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
                        if (usuario.SenhaValida(loginModel.Password))
                        {
                            _sessao.CriarSessaoDoUsuario(usuario);
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
