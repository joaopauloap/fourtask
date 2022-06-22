namespace ProjetoFourTask.Models
{
    public class Tarefa
    {
        public int TarefaId { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataLimite { get; set; }

        public Tarefa()
        {
            DataCriacao = DateTime.Now;
        }
    }
}
