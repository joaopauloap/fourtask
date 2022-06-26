using Microsoft.EntityFrameworkCore;
using ProjetoFourTask.Areas.Identity.Data;
using ProjetoFourTask.Models;

namespace ProjetoFourTask.Repositories
{
    public class TarefaRepository : ITarefaRepository
    {
        private FourTaskContext _context;

        public TarefaRepository(FourTaskContext context)
        {
            _context = context;
        }

        public void AdicionarTarefa(Tarefa tarefa)
        {
            _context.Tarefas.Add(tarefa);
        }

        public void AtualizarTarefa(Tarefa tarefa)
        {
            _context.Tarefas.Update(tarefa);
        }
        public void RemoverTarefa(Tarefa tarefa)
        {
            _context.Tarefas.Remove(tarefa);
        }

        public Tarefa BuscarTarefaPorId(int tarefaId)
        {
            return _context.Tarefas.Include(t => t.Equipe).Include(u => u.Usuario).Where(t => t.TarefaId == tarefaId).FirstOrDefault();
        }

        public List<Tarefa> ListarTodasTarefas()
        {
            return _context.Tarefas.Include(t => t.Equipe).OrderBy(t => t.DataLimite).ToList();
        }

        public void Salvar()
        {
            _context.SaveChanges();
        }

    }
}
