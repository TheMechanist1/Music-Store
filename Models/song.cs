namespace Music_Store.Models
{
    public class Song
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public Models.Genre Genre { get; set; }
        public Models.Performer Performer { get; set; }
    }
}
