using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Projeto_FourTask.Areas.Identity.Data;
using Projeto_FourTask.Models;
namespace Projeto_FourTask.Controllers
{
    public class EquipeController : Controller
    {
        private FourTaskContext _context;

        public EquipeController(FourTaskContext context)
        {
            _context = context;
        }

        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize][HttpGet]
        public IActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Cadastrar(Equipe equipe)
        {
            _context.Equipes.Add(equipe);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize]
        public IActionResult Listagem()
        {
            var equipes = _context.Equipes.ToList();
            return View(equipes);
        }
    }
}
