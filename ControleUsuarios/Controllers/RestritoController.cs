using ControleUsuarios.Filters;
using Microsoft.AspNetCore.Mvc;

namespace ControleUsuarios.Controllers
{
    [PaginaUsuarioLogado]
    public class RestritoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
