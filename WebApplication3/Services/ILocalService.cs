using miniPrpject_Asp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication3.Services
{
    public interface ILocalService
    {

        List<Local> GetAllLocals();
        void AddLocal(Local local); 
        void UpdateLocal(Local newLocal);
        void DeleteLocal(int id);
        List<Local> getLocalDisponible(int idSeance, int idSemaine, int idAnnee, int idClasse);
    }
}
