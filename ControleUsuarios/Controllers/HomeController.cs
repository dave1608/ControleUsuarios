using System.Diagnostics;
using ControleUsuarios.Filters;
using ControleUsuarios.Models;
using Microsoft.AspNetCore.Mvc;

namespace ControleUsuarios.Controllers
{
    [PaginaUsuarioLogado]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ContatoModel home = new ContatoModel();

            home.Nome = "David Luiz";
            home.Email = "david.luiz@gmail.com";

            return View(home);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
