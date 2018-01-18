using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bodlollio.Models
{
    public class HomeViewModel
    {
        public List<Post> posts = new Bodlollio_Context().PostSet.ToList();
    }
}