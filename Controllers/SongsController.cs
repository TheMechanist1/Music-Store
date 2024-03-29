﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Music_Store.Data;
using Music_Store.Models;


namespace Music_Store.Controllers {


    public class SongsController : Controller
    {
        private readonly Music_StoreContext _context;
        private readonly ShoppingCart _shoppingCart;

        public SongsController(Music_StoreContext context, ShoppingCart shoppingCart)
        {
            _context = context;
            _shoppingCart = shoppingCart;
        }

        // GET: Songs
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

        public IActionResult AddToCart(int songId)
        {
            var selectedSong = _context.Song.FirstOrDefault(s => s.Id == songId);
            if (selectedSong != null)
            {
                var shoppingCartItem = new ShoppingCartItem { Song = selectedSong };

                if (_context.ShoppingCarts.Local.Count == 0)
                {
                    _context.ShoppingCarts.Local.Add(new ShoppingCart());
                }

                _context.ShoppingCarts.Local.First().AddItem(shoppingCartItem); //.AddItem(shoppingCartItem);
                Console.WriteLine("Yests");
                try
                {
                    var changes = _context.SaveChanges();
                } catch
                {
                    Console.Write("");
                }
            }
            return RedirectToAction("ViewCart", "ShoppingCart"); // Redirect to the desired view
        }


        // GET: Songs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Song == null)
            {
                return NotFound();
            }

            var song = await _context.Song
                .FirstOrDefaultAsync(m => m.Id == id);
            if (song == null)
            {
                return NotFound();
            }

            return View(song);
        }

        // GET: Songs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Songs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Genre,Performer")] Song song)
        {
            if (ModelState.IsValid)
            {
                _context.Add(song);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(song);
        }

        // GET: Songs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Song == null)
            {
                return NotFound();
            }

            var song = await _context.Song.FindAsync(id);
            if (song == null)
            {
                return NotFound();
            }
            return View(song);
        }

        // POST: Songs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Genre,Performer")] Song song)
        {
            if (id != song.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(song);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SongExists(song.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(song);
        }

        // GET: Songs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Song == null)
            {
                return NotFound();
            }

            var song = await _context.Song
                .FirstOrDefaultAsync(m => m.Id == id);
            if (song == null)
            {
                return NotFound();
            }

            return View(song);
        }

        // POST: Songs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Song == null)
            {
                return Problem("Entity set 'Music_StoreContext.Song'  is null.");
            }
            var song = await _context.Song.FindAsync(id);
            if (song != null)
            {
                _context.Song.Remove(song);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SongExists(int id)
        {
          return (_context.Song?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

public class ShoppingCartController : Controller
{
    private readonly Music_StoreContext _shoppingCart;

    public ShoppingCartController(Music_StoreContext context)
    {
        _shoppingCart = context;
    }

    public async Task<IActionResult> ViewCart()
    {
        // Fetch the latest shopping cart and its items from the database
        var shoppingCart = await _shoppingCart.ShoppingCarts
                                .Include(c => c.Items)
                                .ThenInclude(i => i.Song)
                                .ToListAsync();

        if (shoppingCart == null)
        {
            return View(new ShoppingCart()); // or redirect to an error page or a relevant action
        }

        // Pass the items to the view
        return View(shoppingCart);
    }

}
