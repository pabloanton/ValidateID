using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Persistance;
using Serilog;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ValidateIdApi.Models;

namespace ValidateIdApi.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IApiService _service;
        private readonly ILogger _logger;

        public ProductsController(IApiService service, ILogger logger)
        {
            this._service = service;
            _logger = logger;

        }

        [HttpPost("add")]
        public async Task<IActionResult> Additems([FromBody] BasketRequest request)
        {
            try
            {
                Basket userBasket;
                var checkuser = await this._service.GetUser(request.userId);
                if (checkuser == null)//Check if the user exits
                {
                    return BadRequest(new { success = false, message ="El usuario con Id "+ request.userId + " no existe" });
                }
                else
                {
                    userBasket = await this._service.GetUserBasket(checkuser);
                    //Create Basket for the user
                    if (userBasket == null)
                    {
                        userBasket = await this._service.CreateBasket(checkuser);
                        this._logger.Information(string.Format("[BASKET CREATED]: Created[{0}], UserID:{1}", DateTime.Now.Date,checkuser.id));
                    }

                    var allProducts = await this._service.GetAllProducts();

                    var productsRequest = request.products.Select(c => c.id).ToArray();
                    var getAllProdcutos = allProducts.Select(y=>y.Id).ToArray();

                    bool isSubset = !productsRequest.Except(getAllProdcutos).Any();

                    if (!isSubset)//Check if a least one product doesnt exists
                    {
                        return BadRequest(new { success = false, message = "Alguno de los productos que esta tratando de agregar a la cesta no existe" });
                    }
                    else
                    {
                        var data = new List<ProductQuanty>();

                        foreach(var item in request.products)
                        {
                            var currentProduct = allProducts.FirstOrDefault(x => x.Id == item.id);
                            data.Add(new ProductQuanty { product = currentProduct, quanty = item.quanty });
                            this._logger.Information(string.Format("[ITEM ADDED TO SHOPPING CART]: Added[{0}],USerID: {1}, ProductID: {2},Quanty: {3},Price[<€{4}>]", DateTime.Now.Date.ToString("yyyy-MM-dd"), checkuser.id, item.id, item.quanty, (double)currentProduct.Price));
                        }

                        if (userBasket.products!= null && userBasket.products.Count > 0)
                        {
                            data.AddRange(userBasket.products);
                        }

                        userBasket.products = data;
                        await this._service.SaveChangesAsync();
                    }

                    return Ok(new { success = true, message ="Productos agregados" });
                }

            }
            catch(Exception e)
            {
                this._logger.Error(e, e.Message);
                return BadRequest(new { success=false , error = e.Message }) ;
            }
           

        }

        [HttpGet("basket")]
        public async Task<IActionResult> GetBasket(int userId)
        {
            try
            {
                var user = await this._service.GetUser(userId);
                if(user == null)
                {
                    return BadRequest(new { success = false, message = "El usuario con id "+userId +" no existe" });
                }
                var basket = await this._service.GetUserBasket(user);
                List<string> products = new List<string>();

                foreach(var item in basket.products)
                {
                    products.Add("-" + item.quanty + " x " + item.product.Name + " // " + item.quanty + " x " + item.product.Price + " = " + "$" + (item.quanty * item.product.Price).ToString("##,#0.00"));
                }
                var data = basket.products.Select(x => new { producto = x.product.Id,  price = x.quanty * x.product.Price }).ToList();

                var result = new
                {
                    CreationDate = basket.creationDate.Date.ToString("yyyy-MM-dd"),
                    Products = products,
                    TotalAmount = data.Sum(x => x.price).ToString("##,#0.00")
                };



                return Ok(new { success=true ,  data  = result });
            }
            catch (Exception e)
            {
                this._logger.Error(e, e.Message);
                return BadRequest(new { success = false, error = e.Message });
            }


        }

    }
}
