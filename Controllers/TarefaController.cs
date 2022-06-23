using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoFourTask.Areas.Identity.Data;
using ProjetoFourTask.Models;

namespace ProjetoFourTask.Controllers
{
    public class TarefaController : Controller
    {
        private FourTaskContext _context;
        private UserManager<Usuario> _userManager;
        public TarefaController(FourTaskContext context, UserManager<Usuario> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [Authorize]
        public IActionResult Index()
        {
            List<Tarefa> tarefas = _context.Tarefas.Include(t=>t.Equipe).OrderByDescending(t=>t.DataLimite).ToList();
            return View(tarefas);
        }

        [Authorize]
        [HttpGet]
        public IActionResult Cadastrar()
        {
            ViewBag.ListaEquipes = _context.Equipes.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Cadastrar(Tarefa tarefa, int equipeSelecionada)
        {
            tarefa.EquipeId = equipeSelecionada;
            _context.Tarefas.Add(tarefa);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize]
        [HttpGet]
        public IActionResult Editar(int tarefaId)
        {
            Tarefa tarefa = _context.Tarefas.Include(t=>t.Equipe).Where(t=>t.TarefaId == tarefaId).FirstOrDefault();
            ViewBag.ListaEquipes = _context.Equipes.ToList();
            return View(tarefa);
        }

        [HttpPost]
        public IActionResult Editar(Tarefa tarefa, int equipeSelecionada)
        {
            tarefa.EquipeId = equipeSelecionada;
            _context.Tarefas.Update(tarefa);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
