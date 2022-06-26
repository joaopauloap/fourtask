using ProjetoFourTask.Areas.Identity.Data;
using ProjetoFourTask.Models;

namespace ProjetoFourTask.Repositories
{
    public interface IEquipeRepository
    {
        public Equipe BuscarEquipePorId(int? equipeId);
        public void AdicionarEquipe(Equipe equipe);
        public List<Equipe> ListarEquipes();
        public bool VerificarSenha(int equipeId, string senha);
        public void Salvar();
    }
}
