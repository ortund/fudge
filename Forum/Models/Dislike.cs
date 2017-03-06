namespace Forum.Models
{
    public class Dislike
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public int UserId { get; set; }
    }
}