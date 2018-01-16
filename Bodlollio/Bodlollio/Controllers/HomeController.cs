using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
/* TODO
 * Fragen:
 * 1) Warum haben Sie sich für gerade für den Hash Algorithmus (Usernamen & Passwort)
 * entschieden?
 * 
 * 2) In der User-Login-Tabelle ist noch ein Feld für die IP-Adresse Reserviert. Welche Attacke lässt
 * sich dadurch verhindern?
 * 
 * 3) Erklären Sie, wie diese Attacke genau funktioniert und inwiefern die Gegenmassnahmen die
 * Attacke vereitelt?
 * 
 */
namespace Bodlollio.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}