using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace miniPrpject_Asp.Models
{
    public class Emploi
    {
        public int id_niveau { get; set; }
        public int Id { get; set; }
        public string startDate { get; set; }
        public string startEnd { get; set; }
        public  ICollection<DetailEmploi> LDE { get; set; }
         

        public int SemaineID { get; set; }
        public  Semaine Semaine { get; set; }
         

        public int AnneeID { get; set; }
        public  Annee Annee { get; set; }
        public int valide { get; set; }

    }
}