using ControleUsuarios.Helper;
using ControleUsuarios.Models;
using ControleUsuarios.Repositorio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
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

        public IActionResult RedefinirSenha()
        {
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

        [HttpPost]
        public IActionResult EnviarLinkRedefinirSenha(RedefinirSenhaModel redefinirSenhaModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UsuarioModel usuario = _usuarioRepositorio.BuscarPorEmailELogin(redefinirSenhaModel.Email, redefinirSenhaModel.Login);

                    if(usuario != null )
                    {
                        string novaSenha = usuario.GerarNovaSenha();
                        _usuarioRepositorio.Alterar(usuario);

                        TempData["MensagemSucesso"] = $"Enviamos para seu e-mail uma nova senha.";
                        return RedirectToAction("Index", "Login");
                    }

                    TempData["MensagemErro"] = $"Não foi possível redefinir sua senha, verifique os dados informados!";
                }

                return View("Index");
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Não foi possível redefinir sua senha, tente novamente, detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }
    }
}
