namespace Forum.Models
{
    public class SaveThreadViewModel
    {
        public int TitleId { get; set; }
        public int UserId { get; set; }
        public string ThreadName { get; set; }
        public string Content { get; set; }
    }
}