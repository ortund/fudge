﻿using System;

namespace Forum.Models
{
    public class ThreadViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public DateTime Date { get; set; }
        public string DateString { get; set; }
        public int PostCount { get; set; }
    }
}