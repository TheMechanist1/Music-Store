using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Music_Store.Models;

namespace Music_Store.Data
{
    public class Music_StoreContext : DbContext
    {
        public Music_StoreContext (DbContextOptions<Music_StoreContext> options)
            : base(options)
        {
        }

        public DbSet<Music_Store.Models.Song> Song { get; set; } = default!;
    }
}
