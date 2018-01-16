using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bodlollio.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Dashboard
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Dashboard()
        {

            var current_user = (string)Session["username"];
            //var user_roles = MvcApplication.UserRoles;
            //var current_user_role = (string)user_roles[current_user];
            var current_user_role = "asdf";

            if (current_user_role == "Administrator")
            {

                // access granted

            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }
    }
}