﻿namespace Forum.Models
{
    public class NewThreadViewModel
    {
        public Title Title { get; set; }
        public User User { get; set; }
        public string ThreadName { get; set; }
        public string Content { get; set; }
    }
}