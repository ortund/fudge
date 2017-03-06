namespace Forum.Models
{
    public class Post : BaseObject
    {
        public int ThreadId { get; set; }
        public Thread Thread { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public string Content { get; set; }
        public string DateString { get; set; }
    }
}