using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using miniPrpject_Asp.Models;
using Rotativa.AspNetCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Data;

namespace miniPrpject_Asp.Controllers
{
    public class ClientController : Controller
    {

        private readonly ApplicationDbContext _context;

        public ClientController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }




        public async Task<IActionResult> afficherEmploies()
        {

            ViewBag.Niveau = _context.Classes;
            ViewBag.annee = _context.Annees;
            ViewBag.Semaine = _context.Semaines;
            return View(await _context.Emplois.ToListAsync());
        }




        public ActionResult ParClasse()
        {


            ViewBag.semaines = new SelectList(_context.Semaines, "Id", "NomSemaine");
            ViewBag.classes = new SelectList(_context.Classes, "Id", "code");
            ViewBag.annees = new SelectList(_context.Annees, "Id", "Nom");
            return View();
        }
        [HttpPost]
        public ActionResult AffichageParClasse(string sub)
        {
            ViewBag.local = _context.Locals;
            ViewBag.cours = _context.Cours;
            ViewBag.prof = _context.Professeurs;
            ViewBag.niveau = _context.Classes;
            int idclasse = int.Parse(Request.Form["classe"]);
            int idsemaine = int.Parse(Request.Form["semaine"]);
            int idannee = int.Parse(Request.Form["annee"]);



            var emploi_id = _context.Emplois.Where(
                         x =>
                         x.AnneeID == idannee &&
                         x.SemaineID == idsemaine &&
                            x.id_niveau == idclasse).Select(x =>
                   x.Id).Distinct().ToList();
            List<DetailEmploi> detail = new List<DetailEmploi>();

            foreach (var idEm in emploi_id)
            {
                var detail2 = _context.DetailEmplois.Where(
                x => x.EmploiID == idEm).ToList();

                detail.AddRange(detail2);
            }




            List<infosSeance> detailPDF = new List<infosSeance>();

            foreach (var seance in detail)
            {
                infosSeance el = new infosSeance();
                el.Id = seance.Id;
                el.SeanceID = seance.SeanceID;
                el.EmploiID = seance.EmploiID;
                el.LocalID = seance.LocalID;
                el.CoursID = seance.CoursID;
                foreach (var cour in _context.Cours)
                {
                    if (cour.Id == seance.CoursID)
                    {
                        el.Cours = cour.Nom;

                        foreach (var prof in _context.Professeurs)
                        {
                            if (prof.Id == cour.ProfesseurID)
                            {
                                el.Prof = prof.Nom;
                            }

                        }
                    }
                }
                foreach (var lcl in _context.Locals)
                {
                    if (lcl.Id == seance.LocalID)
                    {
                        el.Local = lcl.Nom;
                    }
                }

                detailPDF.Add(el);

            }





            if (sub == "Afficher")
            {
                return View(detail);
            }
            else
            {
                return new ViewAsPdf("AffichageParClassePdf", detailPDF);
            }
        }


        public ActionResult ParLocal()
        {

            ViewBag.semaines = new SelectList(_context.Semaines, "Id", "NomSemaine");
            ViewBag.locals = new SelectList(_context.Locals, "Id", "Nom");
            ViewBag.annees = new SelectList(_context.Annees, "Id", "Nom");
            return View();
        }
        [HttpPost]
        public ActionResult AffichageParLocal(int semaine, int local, int annee, string sub)
        {
            ViewBag.local = _context.Locals;
            ViewBag.cours = _context.Cours;
            ViewBag.prof = _context.Professeurs;
            ViewBag.niveau = _context.Classes;
            var emploi_id = _context.Emplois.Where(
                           x =>
                           x.AnneeID == annee &&
                           x.SemaineID == semaine).Select(x =>
                     x.Id).Distinct().ToList();
            List<DetailEmploi> detail = new List<DetailEmploi>();

            foreach (var idEm in emploi_id)
            {
                var detail2 = _context.DetailEmplois.Where(
                x => x.EmploiID == idEm &&
                x.LocalID == local).ToList();

                detail.AddRange(detail2);
            }

            List<infosSeance> detailPDF = new List<infosSeance>();

            foreach (var seance in detail)
            {
                infosSeance el = new infosSeance();
                el.Id = seance.Id;
                el.SeanceID = seance.SeanceID;
                el.EmploiID = seance.EmploiID;
                el.LocalID = seance.LocalID;
                el.CoursID = seance.CoursID;
                foreach (var cour in _context.Cours)
                {
                    if (cour.Id == seance.CoursID)
                    {
                        el.Cours = cour.Nom;

                        foreach (var prof in _context.Professeurs)
                        {
                            if (prof.Id == cour.ProfesseurID)
                            {
                                el.Prof = prof.Nom;
                            }

                        }
                    }
                }
                foreach (var lcl in _context.Locals)
                {
                    if (lcl.Id == seance.LocalID)
                    {
                        el.Local = lcl.Nom;
                    }
                }

                detailPDF.Add(el);

            }


            if (sub == "Afficher")
            {
                return View(detailPDF);
            }


            else
            {


                return new ViewAsPdf("AffichageParLocalPdf", detailPDF);
            }
        }



    }

}
