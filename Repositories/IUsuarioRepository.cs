using ProjetoFourTask.Areas.Identity.Data;

namespace ProjetoFourTask.Repositories
{
    public interface IUsuarioRepository
    {
        public Usuario BuscarUsuarioPorId(string usuarioId);
        public void AtualizarUsuario(Usuario usuario);
        public void Salvar();

    }
}
