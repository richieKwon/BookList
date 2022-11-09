using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols;

namespace BookModels
{
    public class BookDbContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        
        public BookDbContext()
        {
            // empty
        }

        public BookDbContext(DbContextOptions<BookDbContext> options) 
            : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().Property(m => m.Created).HasDefaultValueSql("GetDate()");
        }
    }
}