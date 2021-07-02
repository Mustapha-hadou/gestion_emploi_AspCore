using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace miniPrpject_Asp.Models
{
    public class Admin
    {

        public int Id { get; set; }
        public string Nom { get; set; }

        public string Prenom { get; set; }
        public string UserName { get; set; }

        public string Password { get; set; }
    }
}