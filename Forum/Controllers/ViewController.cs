using Forum.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Forum.Utility;
using Humanizer;
using System.Data.Entity;
using System.Text.RegularExpressions;
using HeyRed.MarkdownSharp;

namespace Forum.Controllers
{
    public class ViewController : BaseController
    {
        private int PostsPerPage = 10;
        // GET: View
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Title(int id, int page = 1)
        {
            // create a list of existing threads
            List<Thread> DbThreads = new List<Thread>();
            if (page == 1)
            {
                DbThreads = db.Threads.Where(x => x.TitleId == id && x.Deleted == false).OrderByDescending(x => x.CreatedOn).Take(PostsPerPage).ToList();
            }
            else
            {
                DbThreads = db.Threads.Where(x => x.TitleId == id && x.Deleted == false).OrderByDescending(x => x.CreatedOn).Take(PostsPerPage).Skip(PostsPerPage * page).ToList();
            }

            // populate a list of ViewModels containing the threads
            List<ThreadViewModel> Threads = new List<ThreadViewModel>();
            foreach (Thread Thread in DbThreads)
            {
                Threads.Add(new ThreadViewModel
                {
                    Author = db.Users.FirstOrDefault(x => x.Id == Thread.UserId).Username,
                    Id = Thread.Id,
                    Date = Thread.CreatedOn,
                    DateString = Thread.CreatedOn.Humanize(true, DateTime.UtcNow),
                    Name = Thread.Name,
                    PostCount = db.Posts.Where(x => x.ThreadId == Thread.Id && x.Deleted == false).Count()
                });
            }

            // populate a ViewModel for the current View to use
            ListThreadsViewModel ThreadList = new ListThreadsViewModel
            {
                TitleId = id,
                TitleName = db.Titles.FirstOrDefault(x => x.Id == id).Name,
                Threads = Threads,
                Pages = DbThreads.Count / PostsPerPage,
                CurrentPage = page + 1
            };

            return View(ThreadList);
        }

        public ActionResult Thread(int id, int page = 1)
        {
            // get data specific to the thread
            var Thread = db.Threads.Include(tx => tx.Title).FirstOrDefault(x => x.Id == id);

            // determine how many posts exist in the thread
            int TotalPosts = db.Posts.Where(x => x.ThreadId == id).Count();

            // get posts for this thread
            List<Post> Posts = new List<Post>();
            if (page == 1)
            {
                // if we're on page 1, don't skip any posts
                Posts = db.Posts.Include(tx => tx.User).Where(x => x.ThreadId == id).OrderBy(x => x.CreatedOn).Take(PostsPerPage).ToList();
            }
            else
            {
                // if we're on page 2 or up, skip all the previous posts
                Posts = db.Posts.Include(tx => tx.User).Where(x => x.ThreadId == id).OrderBy(x => x.CreatedOn).Skip(PostsPerPage * (page - 1)).Take(PostsPerPage).ToList();
            }

            List<Opinion> Opinions = new List<Opinion>();

            // strip out <script> & <img> tags from any posts that may include them
            // and get the likes and dislikes for each post
            foreach (Post Post in Posts)
            {
                Regex RemScript = new Regex(@"<script[^>]*>[\s\S]*?</script>");
                Post.Content = RemScript.Replace(Post.Content, "");
                RemScript = new Regex(@"<img[^>]*?/>");
                Post.Content = RemScript.Replace(Post.Content, "");

                Markdown Markdown = new Markdown();
                Post.Content = Markdown.Transform(Post.Content);

                Post.DateString = Post.CreatedOn.Humanize(true, DateTime.UtcNow);

                Opinions.AddRange(db.Opinions.Where(x => x.PostId == Post.Id));
            }

            var Model = new ThreadPostsViewModel
            {
                Thread = Thread,
                Posts = Posts,
                Opinions = Opinions,
                Pages = Convert.ToInt32(Math.Round((decimal)TotalPosts / (decimal)PostsPerPage, 0)),
                CurrentPage = page + 1,
            };

            return View(Model);
        }

        public ActionResult Test()
        {
            Thread Thread = new Thread
            {
                CreatedBy = db.Users.FirstOrDefault(x => x.Id == 1).Username,
                CreatedOn = DateTime.UtcNow,
                Name = "Example thread to demonstrate how posts will appear",
                Title = db.Titles.FirstOrDefault(x => x.Id == 1),
                TitleId = 1,
                UserId = 1
            };
            db.Threads.Add(Thread);
            db.SaveChanges();

            return RedirectToAction("Title", 1);
        }
    }
}