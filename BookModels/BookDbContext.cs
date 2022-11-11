using System;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols;

namespace BookModels
{
    public class BookDbContext : DbContext
    {
        public DbSet<Book> Books { get; set; }

        public BookDbContext()
        {
            
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