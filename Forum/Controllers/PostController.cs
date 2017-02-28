using Forum.Models;
using HeyRed.MarkdownSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Text;


namespace Forum.Controllers
{
    public class PostController : BaseController
    {
        public ActionResult New(int threadId, int quoted = 0)
        {
            if (!CheckUserCookie()) return RedirectToAction("Login", "Home", new { newpost = "true", thread = threadId });

            int UserId = Convert.ToInt32(Request.Cookies["Fudge"]["uid"]);
            bool Banned = Convert.ToBoolean(Request.Cookies["Fudge"]["ub"]);

            if (Banned) return RedirectToAction("Home", "Banned");
            NewPostViewModel Model = new NewPostViewModel
            {
                User = db.Users.FirstOrDefault(x => x.Id == UserId),
                Thread = db.Threads.Include(t => t.Title).FirstOrDefault(x => x.Id == threadId)
            };

            // if this post is a reply quoting another post, add the quoted post to the
            // new post's content - get it by the post id
            if (quoted >= 1)
            {
                Post Parent = db.Posts.Include(t => t.User).FirstOrDefault(x => x.Id == quoted);
                StringBuilder sb = new StringBuilder();
                sb.AppendLine(Parent.Content.Replace(Environment.NewLine, ">"));

                Model.Content = sb.ToString();
            }
            else if (quoted == 0)
            {
                // this quote isn't in reply to an existing post
                Model.Content = String.Empty;
            }

            return View(Model);
        }
        [HttpPost]
        public JsonResult New(SavePostViewModel source)
        {
            try
            {
                // get relevant user/thread info
                User User = db.Users.FirstOrDefault(x => x.Id == source.UserId);
                Thread Thread = db.Threads.FirstOrDefault(x => x.Id == source.ThreadId);

                // create the post
                Post Post = new Post
                {
                    ThreadId = source.ThreadId,
                    CreatedBy = User.Username,
                    CreatedOn = DateTime.UtcNow,
                    Thread = Thread,
                    User = User,
                    UserId = source.UserId,
                    Content = source.Content
                };

                // save the post
                db.Posts.Add(Post);
                db.SaveChanges();

                // determine which page on the thread to redirect the user to
                var PageNumber = Math.Ceiling((double)Post.Id / 10);

                // send the data back to the jquery
                return CreateResponseMessage(Json(new { IsOk = true, PostId = Post.Id, PageNumber = PageNumber }), true);
            }
            catch (Exception ex)
            {
                return ReportError(ex, "Post on Thread");
            }
        }

        [HttpPost]
        public JsonResult Like(int postId)
        {
            try
            {
                if (!CheckUserCookie()) return CreateResponseMessage(Json(new { IsOk = false, RequireLogin = true }), true);
                
                int UserId = Convert.ToInt32(Request.Cookies["Fudge"]["uid"]);
                Opinion Like = new Opinion()
                {
                    Direction = true,
                    PostId = postId,
                    UserId = UserId
                };

                Opinion PostOpinions = db.Opinions.FirstOrDefault(x => x.PostId == postId && x.UserId == UserId);
                if (PostOpinions != null && PostOpinions.Direction)
                {
                    // user has previously Liked the post, remove the Like
                    db.Opinions.Remove(Like);
                    db.SaveChanges();

                    return CreateResponseMessage(Json(new { IsOk = true, PostId = postId, Liked = false }), true);
                }
                else
                {
                    db.Opinions.Add(Like);
                    return CreateResponseMessage(Json(new { IsOk = true, PostId = postId, Liked = true }), true);
                }                
            }
            catch (Exception ex)
            {
                return ReportError(ex, "Like");
            }
        }

        [HttpPost]
        public JsonResult Dislike(int postId)
        {
            try
            {
                if (!CheckUserCookie()) return CreateResponseMessage(Json(new { IsOk = false, RequireLogin = true }), true);

                int UserId = Convert.ToInt32(Request.Cookies["Fudge"]["uid"]);
                Opinion Dislike = new Opinion()
                {
                    Direction = false,
                    PostId = postId,
                    UserId = UserId
                };

                Opinion PostOpinions = db.Opinions.FirstOrDefault(x => x.PostId == postId && x.UserId == UserId);
                if (PostOpinions != null && !PostOpinions.Direction)
                {
                    // user has previously Liked the post, remove the Like
                    db.Opinions.Remove(Dislike);
                    db.SaveChanges();

                    return CreateResponseMessage(Json(new { IsOk = true, PostId = postId, Liked = true }), true);
                }
                else
                {
                    db.Opinions.Add(Dislike);
                    return CreateResponseMessage(Json(new { IsOk = true, PostId = postId, Liked = false }), true);
                }
            }
            catch (Exception ex)
            {
                return ReportError(ex, "Dislike");
            }
        }
    }
}