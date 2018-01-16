using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bodlollio.Models
{

    

    /* Logging_Intrusion_Detection */
    public class HomeControllerViewModel : Controller
    {
        public string UserId { get; internal set; }
        public string LogId { get; internal set; }
        public string LogCreatedOn { get; internal set; }
        
        public ActionResult Logs()
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\juerg.nietlispach\\source\\repos\\Logging_Intrusion_Detection\\Logging_Intrusion_Detection\\Logging_Intrusion_Detection\\App_Data\\logging_intrusion_detection.mdf;" +
                "Integrated Security=True;Connect Timeout=30";

            // check the credentails from the database - caution: SQL Injection should be prevented additionally!
            SqlCommand cmd_credentials = new SqlCommand();
            cmd_credentials.CommandText = "SELECT * FROM [dbo].[UserLog] ul JOIN [dbo].[User] u ON ul.UserId = u.Id ORDER BY ul.CreatedOn DESC";
            cmd_credentials.Connection = con;

            con.Open();

            SqlDataReader reader = cmd_credentials.ExecuteReader();

            if (reader.HasRows) // ok - result was found
            {
                // map the db-results into a list so razor can iterate over the results
                List<HomeControllerViewModel> model = new List<HomeControllerViewModel>();
                while (reader.Read())
                {
                    var log_entry = new HomeControllerViewModel(); // custom created view model

                    // 0 = LogId, 1 = UserId, 2 = IP
                    // 3 = Browser 4 = Action, 5 = Result, 6 = AdditionalInformation
                    // 7 = CreatedOn, 8 = ModifiedOn, 9 = Deletedon
                    // 10 = Id, 11 = Username, 12 = Password

                    log_entry.UserId = reader.GetValue(10).ToString();
                    log_entry.LogId = reader.GetValue(0).ToString();
                    log_entry.LogCreatedOn = reader.GetValue(7).ToString();
                    // to be continued ...

                    model.Add(log_entry);
                }

                return View(model);

            }
            else
            {
                ViewBag.message = "No Results found";
                return View();
            }
        }

        public ActionResult DoLogin()
        {
            // get login-form-data
            var username = Request["username"];
            var password = Request["password"];

            // get additional infos about the request - if set properly by client.
            var ip = Request.ServerVariables["REMOTE_ADDR"];
            var platform = Request.Browser.Platform;
            var browser = Request.UserAgent;

            // connection to database
            SqlConnection con = new SqlConnection();
            con.ConnectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\juerg.nietlispach\\source\\repos\\Logging_Intrusion_Detection\\Logging_Intrusion_Detection\\App_Data\\logging_intrusion_detection.mdf;" +
                "Integrated Security=True;Connect Timeout=30";

            // check the credentails from the database - caution: SQL Injection should be prevented additionally!
            SqlCommand cmd_credentials = new SqlCommand();
            cmd_credentials.CommandText = "SELECT [Id] FROM [dbo].[User] WHERE [Username] = '" + username + "' AND [Password] = '" + password + "'";
            cmd_credentials.Connection = con;

            con.Open();

            SqlDataReader reader_credentials = cmd_credentials.ExecuteReader();

            if (reader_credentials.HasRows) // ok - result was found
            {
                var user_id = 0;
                while (reader_credentials.Read())
                {
                    user_id = reader_credentials.GetInt32(0); // get the user id
                    break;
                }

                con.Close();
                con.Open();

                // check, whether user uses a known browser?
                SqlCommand cmd_user_using_usual_browser = new SqlCommand();
                cmd_user_using_usual_browser.CommandText = "SELECT Id FROM [dbo].[UserLog] WHERE [UserId] = '" +
                                                user_id + "' AND [IP] LIKE '" + ip.Substring(0, 2) + "%' AND browser LIKE '" + platform + "%'";
                cmd_user_using_usual_browser.Connection = con;

                SqlDataReader reader_usual_browser = cmd_user_using_usual_browser.ExecuteReader();

                if (!reader_usual_browser.HasRows)
                {
                    // -> inform user that he / she is maybe not using a usual browser and is accessing the application from a different ip range i.e. from abroad
                    // both signs, that this login is not done by a valid user -> credentials stolen?

                    con.Close();
                    con.Open();

                    // log this user-behaviour anyway
                    SqlCommand log_cmd = new SqlCommand();
                    log_cmd.CommandText = "INSERT INTO [dbo].[UserLog] (UserId, IP, Action, Result, CreatedOn, Browser, AdditionalInformation) VALUES('" + user_id + "', '" +
                        ip + "', 'login', 'success', GETDATE(), '" + platform + "', 'other browser')";
                    log_cmd.Connection = con;
                    log_cmd.ExecuteReader();
                }
                else
                {

                    con.Close();
                    con.Open();

                    // everything should be fine
                    // log this user-behaviour
                    // log this user-behaviour anyway
                    SqlCommand log_cmd = new SqlCommand();
                    log_cmd.CommandText = "INSERT INTO [dbo].[UserLog] (UserId, IP, Action, Result, CreatedOn, Browser) VALUES('" + user_id + "', '" +
                        ip + "', 'login', 'success', GETDATE(), '" + platform + "')";
                    log_cmd.Connection = con;
                    log_cmd.ExecuteReader();
                }
            }
            else
            {
                // credentials do not match 
                // check whether a user can be found at least upon username

                con.Close();
                con.Open();

                SqlCommand cmd_userid_by_name = new SqlCommand();

                cmd_userid_by_name.CommandText = "SELECT [Id] FROM [dbo].[User] WHERE [Username] = '" + username + "'";
                cmd_userid_by_name.Connection = con;

                SqlDataReader reader_userid_by_name = cmd_userid_by_name.ExecuteReader();

                if (reader_userid_by_name.HasRows) // user has been found
                {
                    var user_id = 0;
                    while (reader_credentials.Read())
                    {
                        user_id = reader_credentials.GetInt32(0); // get the user id
                        break;
                    }

                    con.Close();
                    con.Open();

                    // check, whether user has already 5 login attempts
                    // or password does by far not match the systems reccommendations
                    // => Block the user
                    SqlCommand failed_log_cmd = new SqlCommand();
                    failed_log_cmd.CommandText = "SELECT COUNT(ID) FROM [dbo].[UserLog] WHERE UserId = '" + user_id + "' " +
                        "AND Result = 'failed' AND CAST(CreatedOn As date) = '" + System.DateTime.Now.ToShortDateString().Substring(0, 10) + "'";
                    failed_log_cmd.Connection = con;
                    SqlDataReader failed_login_count = failed_log_cmd.ExecuteReader();

                    var attempts = 0;
                    if (failed_login_count.HasRows)
                    {
                        while (reader_credentials.Read())
                        {
                            attempts = reader_credentials.GetInt32(0); // get the count
                            break;
                        }
                    }

                    if (attempts >= 5 || password.Length < 4 || password.Length > 20) // depends on the application context!
                    {
                        // block user!
                    }

                    con.Close();
                    con.Open();

                    // log behaviour anyway
                    // log this user-behaviour anyway
                    SqlCommand log_cmd = new SqlCommand();
                    log_cmd.CommandText = "INSERT INTO [dbo].[UserLog] (UserId, IP, Action, Result, CreatedOn, Browser) " +
                        "VALUES('" + user_id + "', '" + ip + "', 'login', 'failed', GETDATE(), '" + platform + "')";
                    log_cmd.Connection = con;
                    log_cmd.ExecuteReader();

                    ViewBag.Message = "No user found";
                }
                else
                {
                    con.Close();
                    con.Open();

                    // not even the username is correct!
                    // log it with user_id = 0
                    // log this user-behaviour anyway
                    SqlCommand log_cmd = new SqlCommand();
                    log_cmd.CommandText = "INSERT INTO [dbo].[UserLog] (UserId, IP, Action, Result, CreatedOn, AdditionalInformation, Browser) " +
                        "VALUES(0, '" + ip + "', 'login', 'failed', GETDATE(), 'No User Found', '" + platform + "')";
                    log_cmd.Connection = con;
                    log_cmd.ExecuteReader();

                    ViewBag.Message = "No user found";
                }
            }

            con.Close();

            return RedirectToAction("Logs", "Home");
        }
    }
}