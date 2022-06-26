using ProjetoFourTask.Models;

namespace ProjetoFourTask.ViewModels
{
    public class TarefaViewModel
    {
        public Tarefa Tarefa { get; set; }

        public IList<Equipe> ListaEquipes { get; set; }
    }
}
