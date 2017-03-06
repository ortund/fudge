using System.Collections.Generic;

namespace Forum.Models
{
    public class ThreadPostsViewModel
    {
        public Thread Thread { get; set; }
        public List<Post> Posts { get; set; }
        public List<Opinion> Opinions { get; set; }
        public int Pages { get; set; }
        public int CurrentPage { get; set; }
    }
}