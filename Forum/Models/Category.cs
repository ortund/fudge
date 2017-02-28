using Logan.DataFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Forum.Models
{
    public class Category : BaseObject
    {
        public string Name { get; set; }
        public byte[] Image { get; set; }
    }
}