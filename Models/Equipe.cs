namespace Projeto_FourTask.Models
{
    public class Equipe
    {
        public int EquipeId { get; set; }
        public string Nome { get; set; }
        public string Senha { get; set; }
        public string Descricao { get; set; }
        public AreaAtuacao AreaAtuacao { get; set; }
        public DateTime DataCriacao { get; set; }
        
        public Equipe()
        {

        }

    }
}
