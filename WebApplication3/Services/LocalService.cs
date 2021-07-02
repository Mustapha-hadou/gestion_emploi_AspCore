using miniPrpject_Asp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Data;

namespace WebApplication3.Services
{
    public class LocalService : ILocalService
    {

        private ApplicationDbContext _context;

        public LocalService(ApplicationDbContext context)
        {
            _context = context;
        }
        public void AddLocal(Local local)
        {

            _context.Locals.Add(local);
            _context.SaveChanges();
        }

        public void DeleteLocal(int id)
        {
            throw new NotImplementedException();
        }

        public List<Local> GetAllLocals()
        {
            throw new NotImplementedException();
        }

        public List<Local> getLocalDisponible(int idSeance, int idSemaine, int idAnnee, int idClasse)
        {

            var locals = _context.Locals.ToList();
            var emploi_id = _context.Emplois.Where(
               x =>
               x.AnneeID == idAnnee &&
               x.SemaineID == idSemaine).Select(x =>
                 x.Id).ToList();
            List<int> localsNon = new List<int>();
            for (int j = 0; j < emploi_id.Count; j++)
            {
                var id = emploi_id[j];
                List<int> localsNon2 = _context.DetailEmplois.Where(x => x.EmploiID == id && x.SeanceID == idSeance).Select(x =>
                   x.Local.Id).ToList();
                localsNon.AddRange(localsNon2);
            }
            for (int i = 0; i < locals.Count; i++)
            {
                if (localsNon.Contains(locals[i].Id))
                {
                    locals.Remove(locals[i]);
                }
            }
            return locals;

        }

        public void UpdateLocal(Local newLocal)
        {
            throw new NotImplementedException();
        }
    }
}
