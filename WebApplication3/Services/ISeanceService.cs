using miniPrpject_Asp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace miniPrpject_Asp.Services
{
    interface ISeanceService
    {


        List<DetailEmploi> GetAllSeance();

        void AddSeance(DetailEmploi local);

        void UpdateSeance(DetailEmploi newLocal);

        void DeleteSeance(int id);

    }
}
