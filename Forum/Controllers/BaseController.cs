using Forum.Models;
using System;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Forum.Controllers
{
    public class BaseController : Controller
    {
        public ForumEntities db = new ForumEntities();
        // check that the user has a valid login
        public bool CheckUserCookie()
        {
            try
            {
                string Token = Convert.ToString(Request.Cookies["Fudge"]["token"]);
                var Login = db.Logins.Where(x => x.Deleted == false).FirstOrDefault(x => x.Token == Token);

                int UserId = 0;

                // if the user id can't be parsed, or
                // if the login doesn't exist, or
                // if the login user id is not the current user id, then
                //
                // log the user out
                if (!int.TryParse(Request.Cookies["Fudge"]["uid"].ToString(), out UserId) || Login == null || Login.UserId != UserId)
                {
                    ProcessLogout();
                    return false;
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        public void ProcessLogout()
        {
            try
            {
                if (Request.Cookies["Fudge"] != null)
                {
                    string Token = Convert.ToString(Request.Cookies["Fudge"]["token"]);
                    var Login = db.Logins.FirstOrDefault(x => x.Token == Token);

                    if (Login != null)
                    {
                        Login.Deleted = true;
                        db.SaveChanges();
                    }
                }
            }
            catch
            {
                //Error Error = new Error
                //{
                //    Action = "Log out",
                //    Date = DateTime.Now,
                //    Detail = ex.ToString(),
                //    Message = ex.Message,
                //    StackTrace = ex.StackTrace
                //};

                //SaveErrorDetails(Error);
            }
            finally
            {
                Session.Clear();
                HttpCookie UserCookie = new HttpCookie("Fudge");
                UserCookie.Expires = DateTime.Now.AddMonths(-1);
                Response.Cookies.Add(UserCookie);
            }
        }

        // return a response that contains error details in case something broke
        public JsonResult SendResponse(string title, string message, bool isok)
        {
            return Json(new
            {
                Title = title,
                Message = message,
                IsOK = isok
            });
        }
        // return a response simply indicating that the operation executed without errors
        public JsonResult SendResponse(bool isok)
        {
            return Json(new { IsOK = isok });
        }
        // return a response that has an object in it that is required for the next set of operations
        public JsonResult SendResponse(object var, bool isok)
        {
            return Json(new
            {
                Packet = var,
                IsOK = isok
            });
        }

        public JsonResult ReportError(Exception ex, string action = "")
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(String.Format("Error actioning request: [ {0} ]", ex.Message));

            var Result = Json(new
            {
                IsOk = false,
                Title = action,
                Message = ex.Message
            });

            return CreateResponseMessage(Result, true);
        }

        public JsonResult CreateResponseMessage(object data, bool allowGet)
        {
            if (allowGet)
                return Json(data, JsonRequestBehavior.AllowGet);
            else
                return Json(data, JsonRequestBehavior.DenyGet);
        }

        // Always allow GET
        public JsonResult CreateResponseMessage(object data)
        {
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}