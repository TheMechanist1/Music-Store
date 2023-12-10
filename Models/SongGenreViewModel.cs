using Microsoft.AspNetCore.Mvc.Rendering;

namespace Music_Store.Models
{
    public class SongGenreViewModel
    {
        public int Id { get; set; }
        public List<Song>? Songs { get; set; }
        public SelectList? Genres { get; set; }
        public string? SongsGenre { get; set; }
        public string? SearchString { get; set; }
    }
}
