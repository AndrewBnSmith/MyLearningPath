using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyLearningPath.Shared;

namespace MyLearningPath.Server.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookType>().HasData(
                new BookType { Id = 1, Name = "CSharp" },
                new BookType { Id = 2, Name = "Java" }
            );

            modelBuilder.Entity<Book>().HasData(
                new Book { Id = 1, Title = "CSharp Beginner", Author = "Oreilly", BookTypeId = 1 },
                new Book { Id = 2, Title = "Java Beginner", Author = "Camden" , BookTypeId = 2 }
        
            );
        }

        public DbSet<Book> Books { get; set; }

        public DbSet<BookType> BookTypes{ get; set; }
    }
}
