using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using ProjetoFourTask.Models;

namespace ProjetoFourTask.Areas.Identity.Data;

public class Usuario : IdentityUser
{
    //Já gerados pelo identity:
    //public int UsuarioId { get; set; }
    //public string Email { get; set; }
    //public string Senha { get; set; }
    public string Nome { get; set; }
    public DateTime DataNascimento { get; set; }


    //Relacionamentos
    public Equipe Equipe { get; set; }
    public int? EquipeId { get; set; }

}

