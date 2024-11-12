namespace PUT_Backend.Models
{
    //what is sent to the list, not in db alone 
    public class ShortPost
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string Brief { get; set; }
    public int Votes { get; set; }
    public DateTime AddedAt { get; set; }
    public List<Category> Categories { get; set; }
    public bool Anonim { get; set; }
    public string UserId { get; set; }
}

}