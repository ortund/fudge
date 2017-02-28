using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Forum.Models
{
    public class ListThreadsViewModel
    {
        public int TitleId { get; set; }
        public string TitleName { get; set; }
        public List<ThreadViewModel> Threads { get; set; }
        public int Pages { get; set; }
        public int CurrentPage { get; set; }
    }
}