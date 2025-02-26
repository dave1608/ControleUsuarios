using ControleUsuarios.Models;
using ControleUsuarios.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace ControleUsuarios.Controllers
{
    public class ContatoController : Controller
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        public ContatoController(IUsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }
        public IActionResult Index()
        {
            List<UsuarioModel> usuarios = _usuarioRepositorio.BuscarTodos();
            return View(usuarios);
        }

        public IActionResult Criar()
        {
            return View();
        }

        public IActionResult Editar(int id)
        {
            UsuarioModel usuario = _usuarioRepositorio.BuscarPorId(id);
            return View(usuario);
        }

        public IActionResult DeletarConfirmacao(int id)
        {
            UsuarioModel contato = _usuarioRepositorio.BuscarPorId(id);
            return View(contato);
        }

        [HttpPost]
        public IActionResult Criar(UsuarioModel usuario)
        {
            _usuarioRepositorio.Adicionar(usuario);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Alterar(UsuarioModel usuario)
        {
            _usuarioRepositorio.Alterar(usuario);
            return RedirectToAction("Index");
        }
    }
}
