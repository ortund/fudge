using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Forum.Models
{
    public class SavePostViewModel
    {
        public int ThreadId { get; set; }
        public int UserId { get; set; }
        public string Content { get; set; }
    }
}