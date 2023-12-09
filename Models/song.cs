namespace Music_Store.Models
{
    public class song
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int GenreId { get; set; }
        public int PerformerId { get; set; }
    }
}
