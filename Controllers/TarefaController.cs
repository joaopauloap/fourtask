using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ProjetoFourTask.Controllers
{
    public class TarefaController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        [HttpGet]
        public IActionResult Cadastrar()
        {
            return View();
        }

        [Authorize]
        [HttpGet]
        public IActionResult Editar()
        {
            return View();
        }
    }
}
