using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Bodlollio.Controllers
{
    public class SSOController : Controller
    {
        /* Single Sign On */
        public JsonResult CollectUsernamePassword()
        {
            // Get ID_Token and Verify it Using a Library https://developers.google.com/api-client-library/dotnet/apis/oauth2/v2

            // OR check Google Endpoint: https://www.googleapis.com/oauth2/v3/tokeninfo?id_token=XYZ123
            var id_token = Request["idtoken"];
            var request = (HttpWebRequest)WebRequest.Create("https://www.googleapis.com/oauth2/v3/tokeninfo?id_token=" + id_token);

            var postData = "id_token=" + id_token;
            var data = Encoding.ASCII.GetBytes(postData);

            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;

            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            var response = (HttpWebResponse)request.GetResponse();

            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

            // depending on the response: create session or not

            // return the response of googleapis.com
            return Json(responseString);
        }
        // GET: SSO
        public ActionResult Index()
        {
            return View();
        }
    }
}