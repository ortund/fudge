using Logan.DataFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Forum.Models
{
    public class Thread : BaseObject
    {
        public int TitleId { get; set; }
        public Title Title { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
    }
}