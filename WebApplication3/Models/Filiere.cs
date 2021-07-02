using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace miniPrpject_Asp.Models
{
    public class Filiere
    {
       public int Id { get; set; }
       public string Nom { get; set; }
       public  ICollection<Classe> LClasse { get; set; }
      
    }
}