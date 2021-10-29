using Domain.Models;
using Persistance;
using Serilog;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class ApiService : IApiService
    {
        private IApiRepository _repo;
        private ILogger _looger;

        public ApiService(IApiRepository repo , ILogger logger)
        {
            this._repo = repo;
            this._looger = logger;
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
           return await this._repo.GetAllUsers();
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            return await this._repo.GetAllProducts();
        }

        public async Task<User> GetUser(int id)
        {
            var result = await this._repo.GetUser(id);

            return result;

        }

        public async Task<Basket> GetUserBasket(User user)
        {
            var result = await this._repo.GetUserBasket(user);
            return result;
        }


        public async Task<Basket> CreateBasket(User user)
        {
            var result = await this._repo.CreateBasket(user);
            return result;

        }

        public async Task SaveChangesAsync()
        {
            await this._repo.SaveChangesAsync();
        }

        public async Task<IEnumerable<Basket>> GetAllBasket()
        {
            return await this._repo.GetAllBasket();
        }
    }
}
