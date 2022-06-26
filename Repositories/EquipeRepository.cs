using Microsoft.EntityFrameworkCore;
using ProjetoFourTask.Areas.Identity.Data;
using ProjetoFourTask.Models;

namespace ProjetoFourTask.Repositories
{
    public class EquipeRepository : IEquipeRepository
    {
        private FourTaskContext _context;
        public EquipeRepository(FourTaskContext context)
        {
            _context = context;
        }

        public void AdicionarEquipe(Equipe equipe)
        {
           _context.Equipes.Add(equipe);
        }

        public Equipe BuscarEquipePorId(int? equipeId)
        {
            return _context.Equipes.Where(e => e.EquipeId == equipeId).Include(u => u.Usuarios).Include(u => u.Tarefas).FirstOrDefault();
        }

        public List<Equipe> ListarEquipes()
        {
            return _context.Equipes.ToList();
        }

        public void Salvar()
        {
            _context.SaveChanges();
        }

        public bool VerificarSenha(int equipeId, string senha)
        {
            return _context.Equipes.Where(e => e.EquipeId == equipeId && e.Senha == senha).Any();
        }

    }
}
