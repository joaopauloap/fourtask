using ProjetoFourTask.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;

namespace ProjetoFourTask.Models
{
    public class Equipe
    {
        public int EquipeId { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public string Senha { get; set; }

        [Required]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [Required]
        [Display(Name = "Área de atuação")]
        public AreaAtuacao AreaAtuacao { get; set; }

        [Required]
        [Display(Name = "Data de criação"), DataType(DataType.Date)]
        public DateTime DataCriacao { get; set; }


        //Relacionamentos
        public ICollection<Usuario>? Usuarios { get; set; }
        public ICollection<Tarefa>? Tarefas { get; set; }


        public Equipe()
        {
            DataCriacao = DateTime.Now;
        }


    }
}
