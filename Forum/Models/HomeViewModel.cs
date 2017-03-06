using System.Collections.Generic;

namespace Forum.Models
{
    public class HomeViewModel
    {
        public string CategoryName { get; set; }
        public List<Title> CategoryTitles { get; set; }
    }
}