using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IApiService
    {
        Task<IEnumerable<User>> GetAllUsers();
        Task<IEnumerable<Product>> GetAllProducts();
        Task<IEnumerable<Basket>> GetAllBasket();
        Task<User> GetUser(int id);
        Task<Basket> GetUserBasket(User user);
        Task<Basket> CreateBasket(User user);
        Task SaveChangesAsync();
    }
}
