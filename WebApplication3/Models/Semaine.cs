using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace miniPrpject_Asp.Models
{
    public class Semaine
    {
        public int Id { get; set; }
        public string NomSemaine { get; set; }
        public virtual ICollection<Emploi> LEmplois{ get; set; }

        
    }
}