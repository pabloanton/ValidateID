using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ValidateIdApi.Models
{
    public class BasketRequest
    {
        public int userId { get; set; }
        public List<ProductRequest> products { get; set; }
    }

    public class ProductRequest
    {
        public long id { get; set; }
        public int quanty { get; set; }
    }
}
