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
            List<Tarefa> tarefas = _context.Tarefas.Include(t=>t.Equipe).OrderBy(t=>t.DataLimite).ToList();
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
            TempData["msg"] = $"Tarefa {tarefa.TarefaId} - \"{tarefa.Titulo}\" cadastrada com sucesso!";
            return RedirectToAction("Index");
        }

        [Authorize]
        [HttpGet]
        public IActionResult Editar(int tarefaId)
        {
            string idUsuarioLogado = _userManager.GetUserId(User);
            Usuario usuario = _context.Usuarios.Find(idUsuarioLogado);
            Tarefa tarefa = _context.Tarefas.Include(t=>t.Equipe).Where(t=>t.TarefaId == tarefaId).FirstOrDefault();

            if (usuario.EquipeId != tarefa.EquipeId)
            {
                TempData["erro"] = $"Você não é da equipe {tarefa.Equipe.Nome}!";
                return RedirectToAction("index");
            }

            ViewBag.ListaEquipes = _context.Equipes.ToList();
            return View(tarefa);
        }

        [HttpPost]
        public IActionResult Editar(Tarefa tarefa, int equipeSelecionada)
        {
            tarefa.EquipeId = equipeSelecionada;
            _context.Tarefas.Update(tarefa);
            _context.SaveChanges();
            TempData["msg"] = $"Tarefa {tarefa.TarefaId} editada com sucesso!";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Remover(int tarefaId)
        {
            Tarefa tarefa = _context.Tarefas.Find(tarefaId);
            _context.Tarefas.Remove(tarefa);
            _context.SaveChanges();
            TempData["msg"] = $"Tarefa {tarefa.TarefaId} removida com sucesso!";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Aceitar(int tarefaId)
        {
            string idUsuarioLogado = _userManager.GetUserId(User);
            Usuario usuario = _context.Usuarios.Where(u=>u.Id == idUsuarioLogado).Include(u=>u.Tarefas).FirstOrDefault();
            Tarefa tarefa = _context.Tarefas.Find(tarefaId);

            tarefa.UsuarioId = idUsuarioLogado;
            tarefa.Usuario = usuario;
            _context.Tarefas.Update(tarefa);

            usuario.Tarefas.Add(tarefa);
            _context.Usuarios.Update(usuario);

            _context.SaveChanges();

            TempData["msg"] = $"Tarefa {tarefa.TarefaId} foi aceita por {usuario.Nome} com sucesso!";
            return RedirectToAction("index","Equipe");
        }
    }
}
