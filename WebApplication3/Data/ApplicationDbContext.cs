using Microsoft.EntityFrameworkCore;
using miniPrpject_Asp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication3.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext (DbContextOptions options)
            : base(options) 
        {
        }

        public DbSet<Admin> Admins { get; set; }
        public DbSet<Classe> Classes { get; set; }
        public DbSet<Cours> Cours { get; set; }

        public DbSet<DetailEmploi> DetailEmplois { get; set; }

        public DbSet<Emploi> Emplois { get; set; }

        public DbSet<Filiere> Filieres { get; set; }

        public DbSet<Local> Locals { get; set; }
        public DbSet<Professeur> Professeurs { get; set; }

        public DbSet<Annee> Annees { get; set; }

        public DbSet<Seance> Seances { get; set; }

        public DbSet<Semaine> Semaines { get; set; }



    }
}
