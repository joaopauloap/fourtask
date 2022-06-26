using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoFourTask.Areas.Identity.Data;
using ProjetoFourTask.Models;
using ProjetoFourTask.Repositories;
using ProjetoFourTask.ViewModels;

namespace ProjetoFourTask.Controllers
{
    public class TarefaController : Controller
    {
        private UserManager<Usuario> _userManager;
        private ITarefaRepository _tarefaRepository;
        private IEquipeRepository _equipeRepository;
        private IUsuarioRepository _usuarioRepository;

        public TarefaController(UserManager<Usuario> userManager, ITarefaRepository tarefaRepository, IEquipeRepository equipeRepository, IUsuarioRepository usuarioRepository)
        {
            _userManager = userManager;
            _tarefaRepository = tarefaRepository;
            _equipeRepository = equipeRepository;
            _usuarioRepository = usuarioRepository;
        }

        [Authorize]
        public IActionResult Index()
        {
            List<Tarefa> tarefas = _tarefaRepository.ListarTodasTarefas();
            return View(tarefas);
        }

        [Authorize]
        [HttpGet]
        public IActionResult Cadastrar()
        {
            TarefaViewModel viewModel = new TarefaViewModel()
            {
                ListaEquipes = _equipeRepository.ListarEquipes()
            };
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Cadastrar(Tarefa tarefa, int equipeSelecionada)
        {
            tarefa.EquipeId = equipeSelecionada;
            _tarefaRepository.AdicionarTarefa(tarefa);
            _tarefaRepository.Salvar();

            TempData["msg"] = $"Tarefa {tarefa.TarefaId} - \"{tarefa.Titulo}\" cadastrada com sucesso!";
            return RedirectToAction("Index");
        }

        [Authorize]
        [HttpGet]
        public IActionResult Editar(int tarefaId)
        {
            string idUsuarioLogado = _userManager.GetUserId(User);
            Usuario usuario = _usuarioRepository.BuscarUsuarioPorId(idUsuarioLogado);
            Tarefa tarefa = _tarefaRepository.BuscarTarefaPorId(tarefaId);

            if (usuario.EquipeId != tarefa.EquipeId)
            {
                TempData["erro"] = $"Você não é da equipe {tarefa.Equipe.Nome}!";
                return RedirectToAction("index");
            }

            TarefaViewModel viewModel = new TarefaViewModel()
            {
                Tarefa = tarefa,
                ListaEquipes = _equipeRepository.ListarEquipes()
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Editar(Tarefa tarefa, int equipeSelecionada)
        {
            tarefa.EquipeId = equipeSelecionada;
            _tarefaRepository.AtualizarTarefa(tarefa);
            _tarefaRepository.Salvar();
            TempData["msg"] = $"Tarefa {tarefa.TarefaId} editada com sucesso!";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Remover(int tarefaId)
        {
            Tarefa tarefa = _tarefaRepository.BuscarTarefaPorId(tarefaId);
            _tarefaRepository.RemoverTarefa(tarefa);
            _tarefaRepository.Salvar();

            TempData["msg"] = $"Tarefa {tarefa.TarefaId} removida com sucesso!";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Aceitar(int tarefaId)
        {
            string idUsuarioLogado = _userManager.GetUserId(User);
            Usuario usuario = _usuarioRepository.BuscarUsuarioPorId(idUsuarioLogado);
            Tarefa tarefa = _tarefaRepository.BuscarTarefaPorId(tarefaId);

            if (tarefa.UsuarioId != null)
            {
                TempData["erro"] = $"A tarefa \"{tarefa.Titulo}\" já foi aceita pelo usuário {tarefa.Usuario?.Nome}.";
                return RedirectToAction("index", "Equipe");
            }

            tarefa.UsuarioId = idUsuarioLogado;
            tarefa.Usuario = usuario;

            _tarefaRepository.AtualizarTarefa(tarefa);

            usuario.Tarefas.Add(tarefa);
            _usuarioRepository.AtualizarUsuario(usuario);

            _tarefaRepository.Salvar();

            TempData["msg"] = $"Tarefa {tarefa.TarefaId} foi aceita por {usuario.Nome} com sucesso!";
            return RedirectToAction("index", "Equipe");
        }
    }
}
