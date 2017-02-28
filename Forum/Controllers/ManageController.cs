using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Forum.Models;

namespace Forum.Controllers
{
    public class ManageController : BaseController
    {
        // GET: Manage
        public ActionResult Index()
        {
            return View();
        }

        // User clicked on the reset link in the email
        public ActionResult ResetPassword(string token)
        {
            if (!String.IsNullOrEmpty(token))
            {
                User User = db.Users.FirstOrDefault(x => x.ResetToken == token);
                if (User != null)
                {
                    HttpCookie c = new HttpCookie("FFReset");
                    c.Value = bool.TrueString;
                    Response.Cookies.Add(c);
                    return RedirectToAction("NewPassword", "User", new { token = token });
                }

                return SendResponse("RESET PASSWORD", "That's embarrassing. The reset token you got was invalid.", false);
            }

            return SendResponse("RESET PASSWORD", "Oops! We seem to have lost your reset token.<br />Please try again.", false);
        }
    }
}