using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Forum.Models
{
    public class NewPostViewModel
    {
        public Thread Thread { get; set; }
        public User User { get; set; }
        public string Content { get; set; }
    }
}