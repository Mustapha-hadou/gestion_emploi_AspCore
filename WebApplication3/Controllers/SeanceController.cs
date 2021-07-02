using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Data;
using miniPrpject_Asp.Models;

namespace miniPrpject_Asp.Controllers
{
    public class SeanceController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SeanceController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Locals
        public ActionResult Index()
        {
            return View(_context.Emplois.ToList());
        }

       

        public async Task<IActionResult> afficherEmploies()
        {

            ViewBag.Niveau = _context.Classes;
            ViewBag.annee = _context.Annees;
            ViewBag.Semaine = _context.Semaines;
            return View(await _context.Emplois.ToListAsync());
        }







       

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var em = await _context.Emplois.FindAsync(id);
            _context.Emplois.Remove(em);
            foreach(var se in _context.DetailEmplois)
            {
                if (se.EmploiID == id)
                {
                    var sean= await _context.DetailEmplois.FindAsync(se.Id);

                    _context.DetailEmplois.Remove(sean);

                }
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(afficherEmploies));
        }
    }
}
