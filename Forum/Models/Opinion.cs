using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Forum.Models
{
    public enum PostAction
    {
        None = 0,
        Dislike = 1,
        Like = 2
    }

    public class Opinion
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public int UserId { get; set; }
        public PostAction Action { get; set; }        
    }
}