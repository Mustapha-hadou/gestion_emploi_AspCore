using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace miniPrpject_Asp.Models
{
    public class DetailEmploi
    {
        public int Id { get; set; }

        

        public int EmploiID { get; set; }
        public  Emploi Emploi { get; set; }

        

        public int SeanceID { get; set; }
        public  Seance Seance { get; set; }

        public int LocalID { get; set; }
        public  Local Local { get; set; }

        public int CoursID{ get; set; }
        public  Cours Cours { get; set; }

      

      
    }
}