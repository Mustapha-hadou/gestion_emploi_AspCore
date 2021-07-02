using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace miniPrpject_Asp.Models
{
    public class Planning
    {
        public string jour { get; set; }
        public string heure { get; set; }
        public string cour { get; set; }
        public string local { get; set; }

        public string professeur { get; set; }

        public Planning(string jour, string heure, string cour, string local, string professeur)
        {
            this.jour = jour;
            this.heure = heure;
            this.cour = cour;
            this.local = local;
            this.professeur = professeur;
        }
    }
}