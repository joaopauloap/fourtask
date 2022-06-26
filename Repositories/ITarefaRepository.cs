using ProjetoFourTask.Models;

namespace ProjetoFourTask.Repositories
{
    public interface ITarefaRepository
    {
        public List<Tarefa> ListarTodasTarefas();
        public Tarefa BuscarTarefaPorId(int tarefaId);
        public void AdicionarTarefa(Tarefa tarefa);
        public void AtualizarTarefa(Tarefa tarefa);
        public void RemoverTarefa(Tarefa tarefa);
        public void Salvar();
    }
}
