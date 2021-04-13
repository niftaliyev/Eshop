using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EShop.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace EShop.Service
{
    public class CartService : ICartServiceTwo
    {
        private readonly EShopDbContext context;
        private readonly IHttpContextAccessor httpContextAccessor;
        public CartService(EShopDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            this.context = context;
            this.httpContextAccessor = httpContextAccessor;
        }
        public void Add(int id)
        {
            Dictionary<int, int> cart;
            if (!httpContextAccessor.HttpContext.Session.Keys.Contains("cart"))
            {
                cart = new Dictionary<int, int>();
                cart.Add(id, 1);
            }
            else
            {
                cart = JsonConvert.DeserializeObject<Dictionary<int, int>>(httpContextAccessor.HttpContext.Session.GetString("cart"));

                if (cart.ContainsKey(id))
                    cart[id]++;
                else
                    cart.Add(id, 1);
            }

            httpContextAccessor.HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(cart));

        }


        public void Clear()
        {
            httpContextAccessor.HttpContext.Session.Clear();
        }

        public IEnumerable<CartItemViewModel> GetProducts()
        {
            IEnumerable<Product> products = new List<Product>();
            if (httpContextAccessor.HttpContext.Session.Keys.Contains("cart"))
            {
                var cart = JsonConvert
                    .DeserializeObject<Dictionary<int, int>>(httpContextAccessor.HttpContext.Session.GetString("cart"));
                products = context.Products.Where(x => cart.Keys.Contains(x.Id));
                return products.Select(x => new CartItemViewModel { Product = x, Amount = cart[x.Id] });
            }
            return new List<CartItemViewModel>();
        }

        public void Remove(int id)
        {

            //httpContextAccessor.HttpContext.Session.Clear();

            if (httpContextAccessor.HttpContext.Session.Keys.Contains("cart"))
            {
                var cart = JsonConvert
                    .DeserializeObject<Dictionary<int, int>>(httpContextAccessor.HttpContext.Session.GetString("cart"));
                var result = cart.FirstOrDefault(x => x.Key == id);
                if (cart[id] > 1)
                    cart[id]--;
                else
                    cart.Remove(result.Key);
                httpContextAccessor.HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(cart));


            }
        }
    }
}
