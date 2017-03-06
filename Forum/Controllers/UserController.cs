using Forum.Models;
using System;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Forum.Controllers
{
    public class UserController : BaseController
    {

        // GET: User
        public ActionResult Index()
        {
            if (Convert.ToBoolean(Request.Cookies["FudgeForum"]["IsAdmin"]))
                return View();
            else
                return RedirectToRoute("Home");
        }

        /* GET: User/<username>
         * Gets the profile of the selected user.
         * Profile page includes an edit button if the user viewing it is the owner of the profile.
        Clicking the edit button hides the "View" form and displays the "Edit" form */
        public ActionResult Index(string username)
        {
            return View(db.Users.Include("Post").FirstOrDefault(x => x.Username == username));
        }

        #region "Password Reset"
        // GET: User/ResetPassword()
        public ActionResult ResetPassword()
        {
            return View();
        }
        /* POST: User/ResetPassword
         * User submitted a password reset request */
        [HttpPost]
        public JsonResult ResetPassword(PasswordResetViewModel model)
        {
            try
            {
                var User = db.Users.FirstOrDefault(x => x.EmailAddress == model.EmailAddress);

                if (User != null)
                {
                    //TODO: add in mail send functionality
                    //return SendPasswordReset(User);
                    throw (new InvalidOperationException("Some functionality is still being worked on. That function doesn't exist yet."));
                }
                else
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { title = "RESET PASSWORD", message = ex.Message, IsOK = false }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult NewPassword(string token)
        {
            var Model = new PasswordResetViewModel
            {
                Token = token
            };
            return View(Model);
        }
        // user reset the password
        [HttpPost]
        public JsonResult NewPassword(PasswordResetViewModel model)
        {
            User User = db.Users.FirstOrDefault(x => x.ResetToken == model.Token);

            if (User != null)
            {
                if (String.Compare(model.Password, model.Confirm, false) == 0)
                {
                    User.Password = model.Password;
                    User.ResetToken = Guid.NewGuid().ToString();
                    db.SaveChanges();

                    Response.Cookies["FFReset"].Value = bool.FalseString;
                    return SendResponse(true);
                }
                else
                {
                    return SendResponse("RESET PASSWORD", "The passwords you entered didn't match.", false);
                }
            }
            else
            {
                return SendResponse("RESET PASSWORD", "Well this is embarrassing. The user that you specified doesn't seem to exist. Sorry about that!", false);
            }
        }

        // TODO Add in email functionality to send email
        //private JsonResult SendPasswordReset(User user)
        //{
        //    StringBuilder sb = new StringBuilder();
        //    sb.AppendLine(String.Format("<p>Hi {0}!</p>", user.Username));
        //    sb.AppendLine("<p>We received a request to reset your password. If you sent this, please confirm your request and proceed with the process by clicking on the link below.<br />If this is the first you're hearing of this, just ignore the email and don't worry because nothing has happened on your account yet :)</p>");
        //    sb.AppendLine(String.Format("<p><a href=\"http://localhost:50359/Manage/ResetPassword?Token={0}\">http://localhost:50359/Manage/ResetPassword?Token={0}</a></p>", user.ResetToken));
        //    sb.AppendLine("<p>See you on the forums!<br />Ortund (Forum Admin)</p>");

        //    Mail Mail = new Mail();
        //    Mail.SendEmail(user.EmailAddress, "Fudge Forum: Password Reset Requested", sb.ToString(), true);

        //    return SendResponse(true);
        //}
        #endregion
    }
}