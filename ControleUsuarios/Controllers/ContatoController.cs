    using ControleUsuarios.Filters;
using ControleUsuarios.Helper;
using ControleUsuarios.Models;
using ControleUsuarios.Repositorio;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ControleUsuarios.Controllers
{
    [PaginaUsuarioLogado]
    public class ContatoController : Controller
    {
        private readonly IContatoRepositorio _contatoRepositorio;
        private readonly ISessao _sessao;
        public ContatoController(IContatoRepositorio contatoRepositorio, ISessao sessao)
        {
            _contatoRepositorio = contatoRepositorio;
            _sessao = sessao;
        }

        public IActionResult Index()
        {
            UsuarioModel usuariologado = _sessao.BuscarSessaoDoUsuario();
            List<ContatoModel> usuarios = _contatoRepositorio.BuscarTodos(usuariologado.Id);
            return View(usuarios);
        }

        public IActionResult Criar()
        {
            return View();
        }

        public IActionResult Editar(int id)
        {
            ContatoModel usuario = _contatoRepositorio.BuscarPorId(id);
            return View(usuario);
        }

        public IActionResult DeletarConfirmacao(int id)
        {
            ContatoModel contato = _contatoRepositorio.BuscarPorId(id);
            return View(contato);
        }

        public IActionResult Deletar(int id)
        {
            try
            {
                ContatoModel usuario = _contatoRepositorio.BuscarPorId(id);
                _contatoRepositorio.Deletar(id);
                TempData["MensagemSucesso"] = "Contato apagado com sucesso !";
                return RedirectToAction("Index");
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos apagar seu contato, tente novamente, detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult Criar(ContatoModel usuario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UsuarioModel usuariologado = _sessao.BuscarSessaoDoUsuario();
                    usuario.UsuarioId = usuariologado.Id;

                    _contatoRepositorio.Adicionar(usuario);

                    TempData["MensagemSucesso"] = "Contato cadastrado com sucesso !";
                    return RedirectToAction("Index");
                }

                return View(usuario);
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos cadastrar seu contato, tente novamente, detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult Alterar(ContatoModel usuario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UsuarioModel usuariologado = _sessao.BuscarSessaoDoUsuario();
                    usuario.UsuarioId = usuariologado.Id;

                    _contatoRepositorio.Alterar(usuario);
                    TempData["MensagemSucesso"] = "Contato alterado com sucesso !";
                    return RedirectToAction("Index");
                }

                return View("Editar", usuario);
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos alterar seu contato, tente novamente, detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }
    }
}
