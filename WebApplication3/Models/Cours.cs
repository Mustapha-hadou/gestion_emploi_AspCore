using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace miniPrpject_Asp.Models
{
    public class Cours
    {
        public int Id { get; set; }
        public string Nom { get; set; }

        public string Groupe { get; set; }

        

        public  ICollection<DetailEmploi> LDE { get; set; }

       public int ProfesseurID { get; set; }
        public  Professeur Professeur { get; set; }

        
        public int ClasseID { get; set; }
        public virtual Classe Classe { get; set; }


    }
}