using System.ComponentModel.DataAnnotations;

namespace Projeto_FourTask.Models
{
    public class Equipe
    {
        public int EquipeId { get; set; }
        public string Nome { get; set; }
        public string Senha { get; set; }

        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [Display(Name = "Área de atuação")]
        public AreaAtuacao AreaAtuacao { get; set; }

        [Display(Name = "Data de criação"), DataType(DataType.Date)]
        public DateTime DataCriacao { get; set; }

        public Equipe()
        {

        }

    }
}
