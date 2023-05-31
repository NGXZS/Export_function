using Microsoft.EntityFrameworkCore;

namespace PWAY_ASPNetCore_WebAPI.Models
{
    public class BookDbContext : DbContext
    {
        public BookDbContext(DbContextOptions<BookDbContext> options) : base(options)
        {
        }
        public DbSet<Book> BooksDB { get; set; } // MUST be DB name : get data from DB
    }
}
