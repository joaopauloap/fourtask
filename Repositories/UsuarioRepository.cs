using Microsoft.EntityFrameworkCore;
using ProjetoFourTask.Areas.Identity.Data;

namespace ProjetoFourTask.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private FourTaskContext _context;
        public UsuarioRepository(FourTaskContext context)
        {
            _context = context;
        }

        public void AtualizarUsuario(Usuario usuario)
        {
            _context.Usuarios.Update(usuario);
        }

        public Usuario BuscarUsuarioPorId(string usuarioId)
        {
            return _context.Usuarios.Where(u => u.Id == usuarioId).Include(u => u.Tarefas).FirstOrDefault();
        }

        public void Salvar()
        {
            _context.SaveChanges();
        }
    }
}
