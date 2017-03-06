using Forum.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Forum.Controllers
{
    public class ThreadController : BaseController
    {
        public ActionResult New(int titleId)
        {
            if (!CheckUserCookie()) return RedirectToAction("Login", "Home", new { newthread = "true", title = titleId });

            int UserId = Convert.ToInt32(Request.Cookies["Fudge"]["uid"]);
            bool Banned = Convert.ToBoolean(Request.Cookies["Fudge"]["ub"]);

            if (Banned) return RedirectToAction("Home", "Banned");
            NewThreadViewModel Model = new NewThreadViewModel
            {
                Title = db.Titles.FirstOrDefault(x => x.Id == titleId),
                User = db.Users.FirstOrDefault(x => x.Id == UserId),
                Content = String.Empty,
                ThreadName = String.Empty
            };
            return View(Model);
        }
        [HttpPost]
        public JsonResult New(SaveThreadViewModel source)
        {
            try
            {
                Thread Thread = new Thread
                {
                    TitleId = source.TitleId,
                    Name = source.ThreadName,
                    CreatedBy = db.Users.FirstOrDefault(x => x.Id == source.UserId).Username,
                    CreatedOn = DateTime.UtcNow,
                    Title = db.Titles.FirstOrDefault(x => x.Id == source.TitleId),
                    UserId = source.UserId
                };
                db.Threads.Add(Thread);
                db.SaveChanges();

                int ThreadId = Thread.Id;

                Post Post = new Post
                {
                    Content = source.Content,
                    CreatedBy = Thread.CreatedBy,
                    CreatedOn = Thread.CreatedOn,
                    Thread = Thread,
                    ThreadId = Thread.Id,
                    User = db.Users.FirstOrDefault(x => x.Id == Thread.UserId),
                    UserId = Thread.UserId
                };
                db.Posts.Add(Post);
                db.SaveChanges();

                return CreateResponseMessage(Json(new { IsOk = true, ThreadId = Thread.Id }), true);
            }
            catch (Exception ex)
            {
                return ReportError(ex, "Post Thread");
            }
        }
    }
}