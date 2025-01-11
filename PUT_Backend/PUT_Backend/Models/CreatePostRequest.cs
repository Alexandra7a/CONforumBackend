namespace PUT_Backend.Models
{
    public class CreatePostRequest
    {
        public string Title { get; set; }
        public List<Category> Categories { get; set; }
        public bool Anonim { get; set; }
        public string UserId { get; set; }
        public string Content { get; set; }
    }
}