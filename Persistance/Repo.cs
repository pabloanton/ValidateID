using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Persistance.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance
{
    public class Repo : IRepo
    {
        private readonly ApiContext context;

        public Repo(ApiContext context)
        {
            this.context = context;
        }

            public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await Task.Run(() => this.context.Users.ToList());
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            return await Task.Run(() => this.context.Products.ToList());
        }

        public async Task<User> GetUser(int id)
        {
            var result = await this.context.Users.FindAsync(id);

            return result;

        }

        public async Task<Basket> GetUserBasket(User user)
        {
            var result = await Task.Run(() => this.context.Baskets.Include(p=>p.products).ThenInclude(i=>i.product).Where(x=>x.user.id == user.id).FirstOrDefault());
            return result;
        }


        public async Task<Basket> CreateBasket(User user)
        {
            Basket basket = new Basket { creationDate = DateTime.Now ,user = user  , products = new List<ProductQuanty>() };
            await this.context.Baskets.AddAsync(basket);
            await this.context.SaveChangesAsync();
            return basket;

        }

        public async Task SaveChangesAsync()
        {
           var a =  await Task.Run(() =>this.context.SaveChanges());
        }

        public async Task<IEnumerable<Basket>> GetAllBasket()
        {
            return  await Task.Run(() => this.context.Baskets.ToList());
        }

        public async Task<List<User>> GetAllPost()
        {
            var users = await this.context.Users.Include(u => u.Posts).ToListAsync();
            return users;
        }
    }
}
