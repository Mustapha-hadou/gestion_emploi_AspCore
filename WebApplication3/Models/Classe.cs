using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace miniPrpject_Asp.Models
{
    public class Classe
    {
        public int Id { get; set; }
        public string NomClasse { get; set; }

        public string code { get; set; }
        
        public int FiliereID { get; set; }
        public  Filiere Filiere { get; set; }
        
       

          }
}