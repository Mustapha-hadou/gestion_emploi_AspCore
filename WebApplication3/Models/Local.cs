using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace miniPrpject_Asp.Models
{
    public class Local
    {
        public int Id { get; set; }
        public TypeLocal TypeLocal { get; set; }
        public int Capacite { get; set; }
        public string Nom { get; set; }
        public Localisation localisation { get; set; }

        public ICollection<DetailEmploi> ListeDE { get; set; }


    }
    public enum TypeLocal
    {
        Amphi,
        Salle_de_cours,
        Atelier_informatique,
        Atelier_electronique

    }
    public enum Localisation
    {
        Rez_de_chaussée,
        premiér_étage,
        deuxème_étage,
        Bâtiment_industriel

    }
}