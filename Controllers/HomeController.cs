using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Music_Store.Data;
using Music_Store.Models;
using System.Diagnostics;

namespace Music_Store.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly Music_StoreContext _context;
        public HomeController(ILogger<HomeController> logger, Music_StoreContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index(string searchString, string searchField)
        {
            var songsQuery = _context.Song.AsQueryable();

            if (!String.IsNullOrEmpty(searchString))
            {
                if (searchField == "genre")
                {
                    songsQuery = songsQuery.Where(s => s.Genre.Contains(searchString));
                }
                else if (searchField == "performer")
                {
                    songsQuery = songsQuery.Where(s => s.Performer.Contains(searchString));
                }
            }

            var songs = await songsQuery.ToListAsync();
            return View(songs);
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult artists()
        {
            return View();
        }
        public IActionResult admin() 
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
    public class ShoppingCartController : Controller
    {
        private List<ShoppingCartItem> GetShoppingCart()
        {
            // Replace this with your logic to retrieve shopping cart items
            return new List<ShoppingCartItem>
        {
            new ShoppingCartItem { SongId = 1, Quantity = 2 },
            new ShoppingCartItem { SongId = 3, Quantity = 1 },
            // Add more shopping cart items as needed
        };
        }

        public IActionResult ShoppingCart()
        {
            List<ShoppingCartItem> shoppingCart = GetShoppingCart();

            ViewBag.PresetSongs = new List<dynamic>
    {
        new { Id = 1, Title = "Song 1", Artist = "Artist 1", Price = 9.99 },
        new { Id = 2, Title = "Song 2", Artist = "Artist 1", Price = 7.99 },
        // Just a random set of preset songs
    };

            return View(shoppingCart);
        }

        
        public class ShoppingCartItem
        {
            public int SongId { get; set; }
            public int Quantity { get; set; }
        }
    }

    public class ShoppingCartItem
    {
        public int SongId { get; set; }
        public int Quantity { get; set; }
    }


}
