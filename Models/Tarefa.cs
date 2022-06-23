using ProjetoFourTask.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;

namespace ProjetoFourTask.Models
{
    public class Tarefa
    {
        public int TarefaId { get; set; }
        [Required]
        public string Titulo { get; set; }
        [Required]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }
        [Required]
        public DateTime DataCriacao { get; set; }

        [Required]
        [Display(Name = "Data Limite"), DataType(DataType.Date)]
        public DateTime DataLimite { get; set; }


        //Relacionamentos
        public Usuario? Usuario { get; set; }
        public string? UsuarioId { get; set; }
        public Equipe Equipe { get; set; }
        public int EquipeId { get; set; }

        public Tarefa()
        {
            DataCriacao = DateTime.Now;
        }
    }
}
