using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using miniPrpject_Asp.Models;
using miniPrpject_Asp.Services;
using Rotativa.AspNetCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Data;
using WebApplication3.Services;

namespace WebApplication3.Controllers
{
    public class AdminController : Controller
    {

        private readonly ApplicationDbContext _context;
        private static ApplicationDbContext contex;
        private SeanceService seanceService = new SeanceService();
        private LocalService localService = new LocalService(contex);

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public ActionResult Programer_Emploi()
        {

            ViewBag.semaines = new SelectList(_context.Semaines, "Id", "NomSemaine");
            ViewBag.classes = new SelectList(_context.Classes, "Id", "code");
            ViewBag.annees = new SelectList(_context.Annees, "Id", "Nom");
            return View();
        }
        [HttpPost]
        public ActionResult Programer_emploi_etap1(int classe, int semaine, int annee)
        {
            var emploi_id = _context.Emplois.Where(
                            x =>
                            x.AnneeID == annee &&
                            x.id_niveau == classe &&
                            x.SemaineID == semaine).FirstOrDefault();
            
            ViewBag.annee = annee;
            ViewBag.semaine = semaine;
            ViewBag.classe = classe;
            ViewBag.local = _context.Locals;
            ViewBag.cours = _context.Cours;
            ViewBag.prof = _context.Professeurs;



            if (emploi_id == null)
            {
                List<DetailEmploi> detail_ei = new List<DetailEmploi>();

                return View(detail_ei);
            }
            else
            {
                var detail_emploi = _context.DetailEmplois.Where(x => x.EmploiID == emploi_id.Id).ToList();
                ViewBag.id_emploii = emploi_id.Id;
                return View(detail_emploi);
            }
        }
        [HttpPost]
        public ActionResult Programer_emploe_etap2(int idSeance, int idSemaine, int idAnnee, int idClasse,String bout,int idDetailSeance)
        {

            var locals = _context.Locals.ToList();
            var emploi_id = _context.Emplois.Where(
               x =>
               x.AnneeID == idAnnee &&
               x.SemaineID == idSemaine).Select(x =>
                 x.Id).ToList();
            List<int> localsNon = new List<int>();
            List<DetailEmploi> SeancelocalsNon = new List<DetailEmploi>();
            for (int j = 0; j < emploi_id.Count; j++)
            {
                var id = emploi_id[j];
                List<int> localsNon2 = _context.DetailEmplois.Where(x => x.EmploiID == id && x.SeanceID == idSeance).Select(x =>
                   x.Local.Id).ToList();
                List<DetailEmploi> SeancelocalsNon2 =_context.DetailEmplois.Where(x => x.EmploiID == id && x.SeanceID == idSeance).ToList();
                SeancelocalsNon.AddRange(SeancelocalsNon2);

                localsNon.AddRange(localsNon2);
            }
            for (int i = 0; i < locals.Count; i++)
            {
                if (localsNon.Contains(locals[i].Id))
                {
                    locals.Remove(locals[i]);
                }
            }
            ViewBag.localNonDisp= SeancelocalsNon;
            ViewBag.local = new SelectList(locals, "Id", "Nom");

            //Coursssssssssssùùùùùùùù

            var Proffesseurs = _context.Cours.Where(x => x.ClasseID == idClasse).Select(x =>
                     x.ProfesseurID).Distinct().ToList()
                     ;

            List<int> ProfNon = new List<int>();
            for (int j = 0; j < emploi_id.Count; j++)
            {
                var id = emploi_id[j];
                //List<int> ProfNon2 = _context.DetailEmplois.Where(x => x.EmploiID == id && x.SeanceID == idSeance).Select(x =>
                // x.Cours.Professeur.Id).ToList();
                List<int> coursid = _context.DetailEmplois.Where(x => x.EmploiID == id && x.SeanceID == idSeance).Select(x =>
                x.CoursID).ToList();
                List<int> ProfNon2 = new List<int>();
                foreach (var Cour in coursid)
                {
                    foreach (var cours in _context.Cours)
                    {
                        if (cours.Id == Cour)
                        {
                            ProfNon2.Add(cours.ProfesseurID);
                        }
                    }
                }

                ProfNon.AddRange(ProfNon2);
            }
            for (int i = 0; i < Proffesseurs.Count; i++)
            {
                if (ProfNon.Contains(Proffesseurs[i]))
                {
                    Proffesseurs.Remove(Proffesseurs[i]);
                }
            }

            List<Cours> CoursDisponib = new List<Cours>();
            for (int i = 0; i < Proffesseurs.Count; i++)
            {
                int idprof = Proffesseurs[i];

                CoursDisponib.AddRange(_context.Cours.Where(x => x.ClasseID == idClasse && x.ProfesseurID == idprof).ToList());
            }
            ViewBag.cours = new SelectList(CoursDisponib, "Id", "Nom");

            ViewBag.annee = _context.Annees.Where(x => x.Id == idAnnee).First();
            ViewBag.semaine = _context.Semaines.Where(x => x.Id == idSemaine).First();
            ViewBag.seance = _context.Seances.Where(x => x.Id == idSeance).FirstOrDefault();
            ViewBag.classe = _context.Classes.Where(x => x.Id == idClasse).First();

            ViewBag.allCour = _context.Cours;
            ViewBag.allLocal = _context.Locals;
            ViewBag.allProf = _context.Professeurs;


            if (bout == "Ajouter")
            {
               
                ViewBag.AM = 0;
            }

            else if (bout == "Modifier")
            {
                
                ViewBag.idDetailSeance=idDetailSeance;
                ViewBag.AM = 1;
            }
            return View();
        }


        [HttpPost]
        public ActionResult AjoutSeance(string idSemaine, string idSeance, string idAnnee, string idClasse, int Local, int cours,string Seance,int idDetailSeance)
        {

            int idSm = Convert.ToInt32(idSemaine);
            int idSa = Convert.ToInt32(idSeance);
            int idA = Convert.ToInt32(idAnnee);
            int idC = Convert.ToInt32(idClasse);
            if (Seance=="Modifier")
            {
                var seance = _context.DetailEmplois.Where(
                  x =>
                  x.Id == idDetailSeance).FirstOrDefault();
                seance.LocalID = Local;
                seance.CoursID = cours;
                _context.SaveChanges();
            }
            else if (Seance=="Ajouter")
            {

               

                ViewBag.sem = idSemaine;

                var emploi_id = _context.Emplois.Where(
                   x =>
                   x.AnneeID == idA &&
                   x.id_niveau == idC &&
                   x.SemaineID == idSm).FirstOrDefault();

                if (emploi_id == null)
                {

                    Emploi nouveauEmploie = new Emploi();
                    nouveauEmploie.AnneeID = idA;
                    nouveauEmploie.SemaineID = idSm;
                    nouveauEmploie.id_niveau = idC;
                    _context.Emplois.Add(nouveauEmploie);
                    _context.SaveChanges();

                    var emplois = _context.Emplois;
                    int max_id = 0;
                    foreach (var emp in emplois)
                    {
                        if (max_id < emp.Id)
                        {
                            max_id = emp.Id;
                        }
                    }


                    DetailEmploi nouvelleSeance = new DetailEmploi();
                    nouvelleSeance.EmploiID = max_id;
                    nouvelleSeance.SeanceID = idSa;
                    nouvelleSeance.LocalID = Local;
                    nouvelleSeance.CoursID = cours;
                    _context.DetailEmplois.Add(nouvelleSeance);
                    _context.SaveChanges();


                }
                else
                {
                    DetailEmploi nouvelleSeance = new DetailEmploi();
                    nouvelleSeance.EmploiID = emploi_id.Id;
                    nouvelleSeance.SeanceID = idSa;
                    nouvelleSeance.LocalID = Local;
                    nouvelleSeance.CoursID = cours;
                    _context.DetailEmplois.Add(nouvelleSeance);
                    _context.SaveChanges();
                }

            }
           
            
            return RedirectToAction("Programer_emploi_etap1Get", new { classe = idC, semaine = idSm, annee = idA });

        }

        //[HttpGet("{classe}/{semaine}/{annee}")]
        public ActionResult Programer_emploi_etap1Get(string classe, string semaine, string annee)
        {
            int idSm = Convert.ToInt32(semaine);
            int idA = Convert.ToInt32(annee);
            int idC = Convert.ToInt32(classe);

            var emploi_id = _context.Emplois.Where(
                            x =>
                            x.AnneeID == idA &&
                            x.id_niveau == idC &&
                            x.SemaineID == idSm).FirstOrDefault();

            ViewBag.annee = annee;
            ViewBag.semaine = semaine;
            ViewBag.classe = classe;
            ViewBag.local = _context.Locals;
            ViewBag.cours = _context.Cours;
            ViewBag.prof = _context.Professeurs;
            ViewBag.id_emploii = emploi_id;


            if (emploi_id == null)
            {
                List<DetailEmploi> detail_ei = new List<DetailEmploi>();

                return View("Programer_emploi_etap1", detail_ei);
            }
            else
            {
                var detail_emploi = _context.DetailEmplois.Where(x => x.EmploiID == emploi_id.Id).ToList();

                return View("Programer_emploi_etap1", detail_emploi);
            }
        }



        public ActionResult Disponibilite()
        {
            List<SelectListItem> jours = new List<SelectListItem>();
            jours.Add(new SelectListItem { Text = "lundi", Value = "1" });
            jours.Add(new SelectListItem { Text = "mardi", Value = "2" });
            jours.Add(new SelectListItem { Text = "mercredi", Value = "3" });
            jours.Add(new SelectListItem { Text = "jeudi", Value = "4" });
            jours.Add(new SelectListItem { Text = "vendredi", Value = "5" });
            jours.Add(new SelectListItem { Text = "samedi", Value = "6" });

            List<SelectListItem> heures = new List<SelectListItem>();
            heures.Add(new SelectListItem { Text = "8 - 10", Value = "1" });
            heures.Add(new SelectListItem { Text = "10 - 12", Value = "2" });
            heures.Add(new SelectListItem { Text = "12 - 14", Value = "3" });
            heures.Add(new SelectListItem { Text = "14 - 16", Value = "4" });
            heures.Add(new SelectListItem { Text = "16 - 18", Value = "5" });

            ViewBag.jours = jours;
            ViewBag.heures = heures;
            ViewBag.semaines = new SelectList(_context.Semaines, "Id", "NomSemaine");
            ViewBag.annees = new SelectList(_context.Annees, "Id", "Nom");
            return View();
        }
        [HttpPost]
        public ActionResult dispo(int jours, int heures, int semaine, int annee)
        {
            var detail = _context.DetailEmplois.Where(
                x =>
                x.Seance.Id == (jours - 1) * 5 + jours * heures &&
                x.Emploi.AnneeID == annee &&
                x.Emploi.SemaineID == semaine);
            var locals = _context.Locals.Where(x => detail.All(y => y.LocalID != x.Id));
            return View(locals);
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
        public ActionResult ParProfesseur()
        {
            var profs = _context.Professeurs.Select(x => new { x.Id, FullName = x.Nom + " " + x.Prenom });
            ViewBag.semaines = new SelectList(_context.Semaines, "Id", "NomSemaine");
            ViewBag.profs = new SelectList(profs, "Id", "FullName");
            ViewBag.annees = new SelectList(_context.Annees, "Id", "Nom");
            return View();
        }
        [HttpPost]
        public ActionResult AffichageParProfesseur(int semaine, int professeur, int annee, string sub)
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

            var cours_prof = _context.Cours.Where(
                           x =>
                           x.ProfesseurID == professeur).Select(x =>
                     x.Id).Distinct().ToList();
            List<DetailEmploi> detail = new List<DetailEmploi>();
            foreach (var idEm in emploi_id)
            {
                foreach (var idCours in cours_prof)
                {
                    var detail2 = _context.DetailEmplois.Where(
                   x => x.EmploiID == idEm &&
                   x.CoursID == idCours).ToList();
                    detail.AddRange(detail2);
                }
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



            ViewBag.tail = detail;
            if (sub == "Afficher")
            {
                return View(detail);
            }
            else
            {
                return new ViewAsPdf("AffichageParProfesseurPdf", detailPDF);
            }
        }

        [HttpPost]
        public ActionResult statusEmploi(int idEmploi, int idsemaine, string sub,int idAnnee, int idClasse)
        {
            ViewBag.id_emploii = idEmploi;
            ViewBag.semaine = idsemaine;
            ViewBag.annee = idAnnee;
            ViewBag.classe = idClasse;

            if (sub == "valider")
            {
                if (idEmploi == 0 )
                {
                    var emplois = _context.Emplois;
                    int max_id = 0;
                    foreach (var emp in emplois)
                    {
                        if (max_id < emp.Id)
                        {
                            max_id = emp.Id;
                        }
                    }
                    idEmploi = max_id;
                }
                var emploi = _context.Emplois.Where(x => x.Id == idEmploi).FirstOrDefault();
                emploi.valide = 1;
                _context.SaveChanges();

                var semaine = _context.Semaines.Where(x => x.Id > idsemaine);
                return View(semaine);
            }
            return View();



        }

        [HttpPost]
        public ActionResult ConfermerEmploi(List<int> semaines,  int idAnnee, int idClasse,int idEmploi)
        {
            if(idEmploi == 0)
            {
                var emplois = _context.Emplois;
                int max_id = 0;
                foreach (var emp in emplois)
                {
                    if (max_id < emp.Id)
                    {
                        max_id = emp.Id;
                    }
                }
                idEmploi = max_id;
            }
            
            foreach (var i in semaines)
            {
                var emploi_id = _context.Emplois.Where(
               x =>
               x.AnneeID == idAnnee &&
               x.id_niveau == idClasse &&
               x.SemaineID == i).FirstOrDefault();
               if(emploi_id == null)
                {
                    var emplois = _context.Emplois;
                    int max_id = 0;
                    foreach (var emp in emplois)
                    {
                        if (max_id < emp.Id)
                        {
                            max_id = emp.Id;
                        }
                    }
                    Emploi nouveauEmploie = new Emploi();
                    nouveauEmploie.AnneeID = idAnnee;
                    nouveauEmploie.SemaineID = i;
                    nouveauEmploie.id_niveau = idClasse;
                    nouveauEmploie.valide = 1;
                    _context.Emplois.Add(nouveauEmploie);
                    int id = _context.SaveChanges();

                    var detailEmplois = _context.DetailEmplois.Where(x => x.EmploiID == idEmploi);
                    foreach (var detailEploi in detailEmplois)
                    {
                        DetailEmploi nouvelleSeance = new DetailEmploi();
                        nouvelleSeance.EmploiID = max_id + 1;
                        nouvelleSeance.SeanceID = detailEploi.SeanceID;
                        nouvelleSeance.CoursID = detailEploi.CoursID;
                        nouvelleSeance.LocalID = detailEploi.LocalID;
                        _context.DetailEmplois.Add(nouvelleSeance);


                    }
                    _context.SaveChanges();

                }
                else
                {
                    
                    var detailEmplois = _context.DetailEmplois.Where(x => x.EmploiID == emploi_id.Id);
                    foreach(var det in detailEmplois)
                    {
                        _context.DetailEmplois.Remove(det);
                        
                    }
                    _context.SaveChanges();
                    var detailEmploiss = _context.DetailEmplois.Where(x => x.EmploiID == idEmploi);

                    foreach (var detailEploi in detailEmploiss)
                    {
                        DetailEmploi nouvelleSeance = new DetailEmploi();
                        nouvelleSeance.EmploiID = emploi_id.Id;
                        nouvelleSeance.SeanceID = detailEploi.SeanceID;
                        nouvelleSeance.CoursID = detailEploi.CoursID;
                        nouvelleSeance.LocalID = detailEploi.LocalID;
                        _context.DetailEmplois.Add(nouvelleSeance);


                    }
                    _context.SaveChanges();
                }
            }


            return RedirectToAction("afficherEmploies","Seance");


        }




    }
    }

