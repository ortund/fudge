using Forum.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Logan.Utilities;

namespace Forum.Controllers
{
    [RequireHttps]
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            List<Category> Categories = db.Categories.ToList();
            List<Title> Titles = new List<Title>();            
            List<HomeViewModel> Models = new List<HomeViewModel>();

            foreach (Category Cat in Categories)
            {
                Models.Add(new HomeViewModel { CategoryName = Cat.Name, CategoryTitles = db.Titles.Where(x => x.CategoryId == Cat.Id).ToList() });
            }

            return View(Models);
        }
        public ActionResult Login()
        {
            if (Request.QueryString["newthread"] != null && Request.QueryString["newthread"] == "true")
            {
                ViewBag.Redirect = "true";
                ViewBag.Type = "thread";
                ViewBag.Dest = Request.QueryString["title"];
            }
            else if (Request.QueryString["newpost"] != null && Request.QueryString["newpost"] == "true")
            {
                ViewBag.Redirect = "true";
                ViewBag.Type = "post";
                ViewBag.Dest = Request.QueryString["thread"];
            }
            return View(new UserLoginViewModel());
        }
        [HttpPost]
        public JsonResult Login(UserLoginViewModel model)
        {
            try
            {
                if (DoLogin(model.EmailAddress, model.Password))
                {
                    return Json(true, JsonRequestBehavior.AllowGet);
                }

                return ReportError(new Exception("Invalid login credentials"), "Log In");
            }
            catch (Exception ex)
            {
                return ReportError(ex, "Log In");
            }
        }

        private bool DoLogin(string emailAddress, string password)
        {
            var User = db.Users.Include(t => t.Role).Where(x => x.EmailAddress == emailAddress).FirstOrDefault();
            //var User = db.Users.Include("UserRole").Where(x => x.EmailAddress == emailAddress && x.Deleted == false).FirstOrDefault();

            if (User == null) return false;
            if (!Hashing.ValidatePassword(password, User.Password)) return false;

            Login Login = new Login
            {
                UserId = User.Id,
                Token = Guid.NewGuid().ToString(),
                LoginDate = DateTime.Now
            };

            db.Logins.Add(Login);
            db.SaveChanges();

            HttpCookie UserCookie = GenerateCookie(User, Login.Token);
            Response.Cookies.Add(UserCookie);

            return true;
        }
        private HttpCookie GenerateCookie(User user, string token)
        {
            HttpCookie UserCookie = new HttpCookie("Fudge");
            UserCookie.Values["uid"] = Convert.ToString(user.Id);
            UserCookie.Values["uname"] = user.Username;
            UserCookie.Values["role"] = user.Role.Name;
            UserCookie.Values["token"] = token;
            UserCookie.Values["valid"] = bool.TrueString;
            UserCookie.Values["ub"] = Convert.ToString(user.Banned);
            UserCookie.Expires = DateTime.Now.AddMonths(1);

            return UserCookie;
        }

        public ActionResult Logout()
        {
            ProcessLogout();
            return RedirectToAction("Index");
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