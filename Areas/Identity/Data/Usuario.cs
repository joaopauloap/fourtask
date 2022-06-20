using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Projeto_FourTask.Areas.Identity.Data;

public class Usuario : IdentityUser
{
    //Já gerados pelo identity
    //public int UsuarioId { get; set; }
    //public string Email { get; set; }
    //public string Senha { get; set; }
    public DateTime DataNascimento { get; set; }

}

