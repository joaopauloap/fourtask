using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoFourTask.Areas.Identity.Data;
using ProjetoFourTask.Models;
using ProjetoFourTask.Repositories;

namespace ProjetoFourTask.Controllers
{
    public class EquipeController : Controller
    {
        private UserManager<Usuario> _userManager;
        private IEquipeRepository _equipeRepository;
        private IUsuarioRepository _usuarioRepository;

        public EquipeController(UserManager<Usuario> userManager, IEquipeRepository equipeRepository, IUsuarioRepository usuarioRepository)
        {
            _userManager = userManager;
            _equipeRepository = equipeRepository;
            _usuarioRepository = usuarioRepository;
        }

        [Authorize]
        public IActionResult Index()
        {
            string idUsuarioLogado = _userManager.GetUserId(User);
            Usuario usuario = _usuarioRepository.BuscarUsuarioPorId(idUsuarioLogado);
            Equipe equipe = _equipeRepository.BuscarEquipePorId(usuario.EquipeId);

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
            _equipeRepository.AdicionarEquipe(equipe);
            _equipeRepository.Salvar();
            TempData["msg"] = $"Equipe {equipe.Nome} cadastrada com sucesso!";
            return RedirectToAction("Listagem");
        }

        [Authorize]
        public IActionResult Listagem()
        {
            var equipes = _equipeRepository.ListarEquipes();
            return View(equipes);
        }

        [HttpPost]
        public IActionResult Entrar(int equipeId, string senha)
        {
            bool verificarSenha = _equipeRepository.VerificarSenha(equipeId,senha);

            if (verificarSenha == true)
            {
                string idUsuarioLogado = _userManager.GetUserId(User);
                Usuario usuario = _usuarioRepository.BuscarUsuarioPorId(idUsuarioLogado);
                Equipe equipe = _equipeRepository.BuscarEquipePorId(equipeId);

                usuario.EquipeId = equipeId;
                usuario.Equipe = equipe;

                _usuarioRepository.AtualizarUsuario(usuario);
                _usuarioRepository.Salvar();

                TempData["msg"] = $"Você entrou na equipe {usuario.Equipe.Nome}!";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["erro"] = "Erro ao tentar entrar em equipe. Senha incorreta!";
                return RedirectToAction("Listagem");
            }
        }

        [HttpPost]
        public IActionResult Sair()
        {
            string idUsuarioLogado = _userManager.GetUserId(User);
            Usuario usuario = _usuarioRepository.BuscarUsuarioPorId(idUsuarioLogado);

            usuario.EquipeId = null;
            usuario.Equipe = null;

            _usuarioRepository.AtualizarUsuario(usuario);
            _usuarioRepository.Salvar();

            TempData["msg"] = "Você saiu de uma equipe!";
            return RedirectToAction("Index");
        }
    }
}
