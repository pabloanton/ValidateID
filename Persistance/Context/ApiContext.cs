using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Context
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options)
            : base(options)
        {
             Initialize();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(entity =>
            {
                // Set key for entity
                entity.HasKey(p => p.Id);
            });
            modelBuilder.Entity<User>(entity =>
            {
                // Set key for entity
                entity.HasKey(p => p.id);
            });

            modelBuilder.Entity<Basket>(entity =>
            {
                // Set key for entity
                entity.HasKey(p => p.id);
            });

            modelBuilder.Entity<ProductQuanty>(entity =>
            {
                // Set key for entity
                entity.HasKey(p => p.id);
            });


            base.OnModelCreating(modelBuilder);
        }


        private void Initialize()
        {
            if (!Task.Run(() => this.Products.AnyAsync()).Result)
            {
                Product book1 = new Product { Id = 10001, Name = "lord of the Rings", Price = 10.0, Type = "Book" };
                Product book2 = new Product { Id = 10002, Name = "The hobbit", Price = 5.0, Type = "Book" };

                Product dvd1 = new Product { Id = 20001, Name = "GOT", Price = 9.0, Type = "Book" };
                Product dvd2 = new Product { Id = 20110, Name = "Breaking Bad", Price = 7.0, Type = "Book" };

                List<Product> products = new List<Product>();
                products.Add(book1);
                products.Add(book2);
                products.Add(dvd1);
                products.Add(dvd2);

                this.Products.AddRange(products);
                this.SaveChanges();
            }
            User user = null;
            if (!Task.Run(() => this.Users.AnyAsync()).Result)
            {
                user = new User { firstName = "Root", lastName = "ValidateID" };
                this.Users.Add(user);
                this.SaveChanges();
            }

        }

        public DbSet<User> Users { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Basket> Baskets { get; set; }

        public DbSet<ProductQuanty> productQuanties { get; set; }

    }
}
