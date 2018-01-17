using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
/* TODO
 * Fragen:
 * 1) Warum haben Sie sich für gerade für den Hash Algorithmus (Usernamen & Passwort)
 * entschieden? Ein unterschiedlicher "Salt" für jedes neue Passwort garantiert das Angriefer keine vorgenerierte Tabellen mit hashes (Rainbow-Tablles) nutzen können.
 * Wenn 2 Passwörter ähnlich sind werden sie garantiert andere Hashes haben. Wiederholtes aufrufen von SHA-1 heisst das der Angreiffer das auch tun muss.
 * Die dafür gebrauchten Ressourcen um zumbeispiel eine Brute-Force-Attacke durchzuführen sind imenz hoch.
 * Hash-Alogrithmus:
public static string HashPassword(string password)
{
    byte[] salt;
    byte[] buffer2;
    if (password == null)
    {
        throw new ArgumentNullException("password");
    }
    using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, 0x10, 0x3e8))
    {
        salt = bytes.Salt;
        buffer2 = bytes.GetBytes(0x20);
    }
    byte[] dst = new byte[0x31];
    Buffer.BlockCopy(salt, 0, dst, 1, 0x10);
    Buffer.BlockCopy(buffer2, 0, dst, 0x11, 0x20);
    return Convert.ToBase64String(dst);
}
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
        /* Role_Based_Auth */
        public ActionResult Dashboard()
        {

            var current_user = (string)Session["username"];
            // var user_roles = MvcApplication.UserRoles;
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

        /* TWO FACTOR AUTH OTP */
        public ActionResult Login()
        {
            // In order to make this code work -> replace all UPPERCASE-Placeholders with the corresponding data!

            var username = Request["username"];
            var password = Request["password"];

            var mode = "EMAIL"; // OR SMS


            if (username == "test" && password == "test")
            {

                if (mode == "SMS")
                {
                    var request = (HttpWebRequest)WebRequest.Create("https://rest.nexmo.com/sms/json");

                    var secret = "TEST_SECRET";

                    var postData = "api_key=API_KEY";
                    postData += "&api_secret=API_SECRET";
                    postData += "&to=MY_PHONENUMBER";
                    postData += "&from=\"\"NEXMO\"\"";
                    postData += "&text=\"" + secret + "\"";
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

                    ViewBag.Message = responseString;
                }

                if (mode == "EMAIL")
                {

                    var request = (HttpWebRequest)WebRequest.Create("https://api.mailgun.net/v3/DOMAIN_NAME/messages");

                    // Add Basic Auth
                    String encoded = System.Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes("api:API_KEY"));
                    request.Headers.Add("Authorization", "Basic " + encoded);

                    var secret = "TEST_SECRET"; // In Library Funktion auslagern

                    var postData = "from=Test User <mailgun@DOMAIN_NAME>";
                    postData += "&to=MY_AUTHORIZED_RECIPIENT_EMAIL_ADDRESS";
                    postData += "&subject=Secret-Token";
                    postData += "&text=\"" + secret + "\"";
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

                    ViewBag.Message = responseString;

                }

            }
            else
            {
                ViewBag.Message = "Wrong Credentials";
            }

            return View();
        }



        public void TokenLogin()
        {
            var token = Request["token"];

            if (token == "TEST_SECRET")
            {
                // -> "Token is correct";
            }
            else
            {
                // -> "Wrong Token";
            }

        }

        public ActionResult LoginTwoFactor()
        { 

            // In order to make this code work -> replace all UPPERCASE-Placeholders with the corresponding data!

            var username = Request["username"];
            var password = Request["password"];

            var mode = "EMAIL"; // OR SMS
            

            if (username == "test" && password == "test")
            {

                if (mode == "SMS")
                {
                    var request = (HttpWebRequest)WebRequest.Create("https://rest.nexmo.com/sms/json");

                    var secret = "TEST_SECRET";

                    var postData = "api_key=API_KEY";
                    postData += "&api_secret=API_SECRET";
                    postData += "&to=MY_PHONENUMBER";
                    postData += "&from=\"\"NEXMO\"\"";
                    postData += "&text=\"" + secret + "\"";
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

                    ViewBag.Message = responseString;
                }

                if (mode == "EMAIL")
                {
                    
                    var request = (HttpWebRequest)WebRequest.Create("https://api.mailgun.net/v3/DOMAIN_NAME/messages");

                    // Add Basic Auth
                    String encoded = System.Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes("api:API_KEY"));
                    request.Headers.Add("Authorization", "Basic " + encoded);

                    var secret = "TEST_SECRET"; // In Library Funktion auslagern

                    var postData = "from=Test User <mailgun@DOMAIN_NAME>";
                    postData += "&to=MY_AUTHORIZED_RECIPIENT_EMAIL_ADDRESS";
                    postData += "&subject=Secret-Token";
                    postData += "&text=\"" + secret + "\"";
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

                    ViewBag.Message = responseString;
                    
                }

            }
            else
            {
                ViewBag.Message = "Wrong Credentials";
            }                       
            
            return View();
        }
    /* SESSIONS */
    public ActionResult DoLogin()
{

    var username = Request["username"];
    var password = Request["password "];
    var stay_logged_in = Request["stay_logged_in"];

    if (username == "test" && password == "test")
    {

        // Checkboxex are submitted with "on"-value, in case they are checked
        if (stay_logged_in == "on")
        {
            var auth_cookie = new HttpCookie("misleading_name_for_an_authentication_cookie");
            auth_cookie.Value = "SOME_NONCE_VALUE";
            auth_cookie.Expires = DateTime.Now.AddDays(14d);
            auth_cookie.Path = "localhost:49764";

            Response.Cookies.Add(auth_cookie);
            // use a "custom" cookie with an duration of today + 2 Weeks
        }
        else
        {
            // return a "default" ASP.NET SESSION-ID-Cookie
            Session["misleading_name_for_an_authentication_cookie"] = "SOME_NONCE_VALUE"; // creates a session immediately
        }

        //Response.Redirect("Admin/Home");
        return RedirectToAction("home", "admin");
    }
    else
    {
        ViewBag.content = "Failed to login";
    }


    return View();
}

public ActionResult Logout()
{

    // Destroy both: ASP.NET and the Custom Cookie

    // Custom Cookie
    var auth_cookie = new HttpCookie("misleading_name_for_an_authentication_cookie");
    auth_cookie.Value = "SOME_NONCE_VALUE";
    auth_cookie.Expires = DateTime.Now.AddDays(-10); // or set to:  Thu, 01 Jan 1970 00:00:00 GMT
    auth_cookie.Path = "localhost:49764";

    Response.Cookies.Add(auth_cookie);

    // ASP.NET Session
    Session.Clear();

    return RedirectToAction("home", "admin");
    //Response.Redirect("Home/Index");            
}
    }
}