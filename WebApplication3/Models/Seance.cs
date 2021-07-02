using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace miniPrpject_Asp.Models
{
    public class Seance
    {
        public int Id { get; set; }
        public string HeurDebut { get; set; }
        public string HeurFin { get; set; }
        public string jour { get; set; }

        public virtual ICollection<DetailEmploi> LDE { get; set; }

    }
}