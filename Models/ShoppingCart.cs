namespace Music_Store.Models
{
    public class ShoppingCart
    {
        public int Id { get; set; }
        public virtual ICollection<ShoppingCartItem> Items { get; set; }


        public ShoppingCart()
        {
            Items = new List<ShoppingCartItem>();
        }

        // Add an item to the cart
        public void AddItem(ShoppingCartItem item)
        {
            Items.Add(item);
        }
    }
}
