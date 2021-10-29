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
            try
            {
                return await this._repo.GetAllUsers();

            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            try
            {
                return await this._repo.GetAllProducts();

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<User> GetUser(int id)
        {

            try
            {
                var result = await this._repo.GetUser(id);

                return result;

            }
            catch (Exception e)
            {
                throw e;
            }
            

        }

        public async Task<Basket> GetUserBasket(User user)
        {
            try
            {
                var result = await this._repo.GetUserBasket(user);
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }

        }


        public async Task<Basket> CreateBasket(User user)
        {
            try
            {
                var result = await this._repo.CreateBasket(user);
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task SaveChangesAsync()
        {
            try
            {
                await this._repo.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<IEnumerable<Basket>> GetAllBasket()
        {
            return await this._repo.GetAllBasket();
        }
    }
}
