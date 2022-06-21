using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projeto_FourTask.Areas.Identity.Data;
using Projeto_FourTask.Models;
namespace Projeto_FourTask.Controllers
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
            //var usuario = _context.Usuarios.Where(u => u.Id == idUsuarioLogado).Include(u=>u.Equipe).FirstOrDefault();
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
            equipe.DataCriacao = DateTime.Now;
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

        [HttpPost]
        public IActionResult Entrar(int equipeid, string senha)
        {
            bool verificarSenha = _context.Equipes.Where(e => (e.EquipeId == equipeid) && (e.Senha == senha)).Any();
            if (verificarSenha == true)
            {
                string idUsuarioLogado = _userManager.GetUserId(User);
                Usuario usuario = _context.Usuarios.Find(idUsuarioLogado);
                usuario.EquipeId = equipeid;
                usuario.Equipe = _context.Equipes.Find(equipeid);
                _context.Usuarios.Update(usuario);
                _context.SaveChanges();
                return RedirectToAction("Index");

            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
