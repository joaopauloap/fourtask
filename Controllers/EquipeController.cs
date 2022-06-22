using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoFourTask.Areas.Identity.Data;
using ProjetoFourTask.Models;

namespace ProjetoFourTask.Controllers
{
    public class EquipeController : Controller
    {
        private FourTaskContext _context;
        private UserManager<Usuario> _userManager;
        public EquipeController(FourTaskContext context, UserManager<Usuario> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [Authorize]
        public IActionResult Index()
        {
            string idUsuarioLogado = _userManager.GetUserId(User);
            var usuario = _context.Usuarios.Find(idUsuarioLogado);
            var equipe = _context.Equipes.Where(e => e.EquipeId == usuario.EquipeId).Include(u => u.Usuarios).FirstOrDefault();

            return View(equipe);
        }

        [Authorize]
        [HttpGet]
        public IActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Cadastrar(Equipe equipe)
        {
            _context.Equipes.Add(equipe);
            _context.SaveChanges();
            TempData["msg"] = $"Equipe {equipe.Nome} cadastrada com sucesso!";
            return RedirectToAction("Listagem");
        }

        [Authorize]
        public IActionResult Listagem()
        {
            var equipes = _context.Equipes.ToList();
            return View(equipes);
        }

        [HttpPost]
        public IActionResult Entrar(int equipeid, string senha)
        {
            bool verificarSenha = _context.Equipes.Where(e => e.EquipeId == equipeid && e.Senha == senha).Any();
            if (verificarSenha == true)
            {
                string idUsuarioLogado = _userManager.GetUserId(User);
                Usuario usuario = _context.Usuarios.Find(idUsuarioLogado);
                usuario.EquipeId = equipeid;
                usuario.Equipe = _context.Equipes.Find(equipeid);
                _context.Usuarios.Update(usuario);
                _context.SaveChanges();

                TempData["msg"] = $"Você entrou na equipe {usuario.Equipe.Nome}!";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["msg"] = "Erro ao tentar entrar em equipe. Senha incorreta!";
                return RedirectToAction("Listagem");
            }
        }

        public IActionResult Sair()
        {
            string idUsuarioLogado = _userManager.GetUserId(User);
            Usuario usuario = _context.Usuarios.Find(idUsuarioLogado);
            usuario.EquipeId = null;
            usuario.Equipe = null;
            _context.Usuarios.Update(usuario);
            _context.SaveChanges();
            TempData["msg"] = "Você saiu da equipe!";
            return RedirectToAction("Index");
        }
    }
}
