using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Persistance.Context;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance
{
    public class ApiRepository : IApiRepository
    {
        private readonly ApiContext context;
        private readonly ILogger _logger;

        public ApiRepository(ILogger logger , ApiContext context)
        {
            this.context = context;
            this._logger = logger;
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            try
            {
                return await Task.Run(() => this.context.Users.ToList());
            }
            catch (Exception e)
            {
                this._logger.Error(e, e.Message);
                throw e;
            }
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {

            try
            {
                return await Task.Run(() => this.context.Products.ToList());
            }
            catch (Exception e)
            {
                this._logger.Error(e, e.Message);
                throw e;
            }
        }

        public async Task<User> GetUser(int id)
        {

            try
            {
                var result = await this.context.Users.FindAsync(id);
                return result;

            }
            catch (Exception e)
            {
                this._logger.Error(e, e.Message);
                throw e;
            }
        }

        public async Task<Basket> GetUserBasket(User user)
        {
            try
            {
                var result = await Task.Run(() => this.context.Baskets.Include(p => p.products).ThenInclude(i => i.product).Where(x => x.user.id == user.id).FirstOrDefault());
                return result; 
            }
            catch (Exception e)
            {
                this._logger.Error(e, e.Message);
                throw e;
            }
           
        }


        public async Task<Basket> CreateBasket(User user)
        {

            try
            {
                Basket basket = new Basket { creationDate = DateTime.Now, user = user, products = new List<ProductQuanty>() };
                await this.context.Baskets.AddAsync(basket);
                await this.context.SaveChangesAsync();
                return basket;
            }
            catch (Exception e)
            {
                this._logger.Error(e, e.Message);
                throw e;
            }

        }

        public async Task SaveChangesAsync()
        {
            try
            {
                await Task.Run(() => this.context.SaveChanges());
            }
            catch (Exception e)
            {
                this._logger.Error(e, e.Message);
                throw e;
            }
        }

        public async Task<IEnumerable<Basket>> GetAllBasket()
        {
            try
            {
                return await Task.Run(() => this.context.Baskets.ToList());
            }
            catch (Exception e)
            {
                this._logger.Error(e, e.Message);
                throw e;
            }
        }
    }
}
