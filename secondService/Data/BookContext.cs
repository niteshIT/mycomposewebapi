using Microsoft.EntityFrameworkCore;
using secondService.Model;
using System.Collections.Generic;

namespace secondService.Data
{
    public class BookContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public BookContext(DbContextOptions<BookContext> options) : base(options) { }

        
    }
}
