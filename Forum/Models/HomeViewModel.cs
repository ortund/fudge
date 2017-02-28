using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Forum.Models
{
    public class HomeViewModel
    {
        public string CategoryName { get; set; }
        public List<Title> CategoryTitles { get; set; }
    }
}