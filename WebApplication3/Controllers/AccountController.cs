using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Linq;
using WebApplication3.Data;

namespace WebApplication3.Controllers
{

    public class AccountController : Controller
    {

        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Success()
        {
            return View();
        }

    
        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            var admin = _context.Admins.Where(x => x.UserName == username && x.Password == password).FirstOrDefault();

            if (admin!=null )
            {
                HttpContext.Session.SetInt32("id",admin.Id);
                HttpContext.Session.SetString("username",admin.UserName);

                return RedirectToAction("Index","Admin");
            }
            else
            {
                var prof = _context.Professeurs.Where(x => x.UserName == username && x.Password == password).FirstOrDefault();
                if(prof != null)
               {
                    HttpContext.Session.SetInt32("id", prof.Id);
                    HttpContext.Session.SetString("username", prof.UserName);

                    return RedirectToAction("Index", "Client");
                }
                return RedirectToAction("Index", "Client");
            }
        }

        [HttpGet]
        public ActionResult Logout()
        {
            HttpContext.Session.Remove("username");
            return RedirectToAction("Index");
        }
    }
}