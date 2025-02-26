using System.Diagnostics;
using ControleUsuarios.Models;
using Microsoft.AspNetCore.Mvc;

namespace ControleUsuarios.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            UsuarioModel home = new UsuarioModel();

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
