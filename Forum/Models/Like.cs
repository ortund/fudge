using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Forum.Models
{
    public class Like
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public int UserId { get; set; }
    }
}