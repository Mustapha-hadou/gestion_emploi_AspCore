using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace miniPrpject_Asp.Models
{
    public class Annee
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string StartDate { get; set; }
        public string StartEnd { get; set; }
        public  ICollection<Emploi> LEmplois { get; set; }


    }
}