using Microsoft.AspNetCore.Mvc;

namespace Projeto_FourTask.Controllers
{
    public class TarefaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Cadastrar()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Editar() 
        {
            return View();
        }
    }
}
