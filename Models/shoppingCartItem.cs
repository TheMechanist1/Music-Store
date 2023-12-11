namespace Music_Store.Models
{
    public class ShoppingCartItem
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int SongId { get; set; }
        public Song Song { get; set; } // Navigation property to the Song
        public int ShoppingCartId { get; set; }
        public ShoppingCart ShoppingCart { get; set; } // Navigation property to the ShoppingCart
    }
}
