using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bodlollio.Models
{
    public class HomeViewModel : Controller
    {
        // GET: HomeViewModels
        public ActionResult Index()
        {
            return View();
        }
    }
}