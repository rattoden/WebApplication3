using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WebApplication3.Models
{
    public class SongsContext : DbContext
    {
        public DbSet<Song> Song { get; set; }
        public SongsContext(DbContextOptions<SongsContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
