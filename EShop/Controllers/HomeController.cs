using EShop.Models;
using EShop.Service;
using EShop.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly EShopDbContext context;
        private readonly ICartServiceTwo cartService;

        public HomeController(EShopDbContext context , ICartServiceTwo cartService)
        {

            this.context = context;
            this.cartService = cartService;
        }

        public IActionResult Index(int? id)
        {
            if (id != null)
            {
                var result = new IndexModel
                {
                    CategoryId = id,
                    Categories = context.Categories,
                    Products = context.Products.Where(x => x.CategoryId == id).OrderByDescending(x => x.Id).Take(6)

                };
                return View(result);
            }


            var items  = new IndexModel
            {
                Products = context.Products.OrderByDescending(x => x.Id).Take(6 * 1),
                Categories = context.Categories
            };

            return View(items);
        }


        /// <NEW FICHA


        public IActionResult Scroll(int? id ,int page = 0)
        {
            var parperntPage = 6;
            int count;

            if (id != null)
            {
                var result = new IndexModel
                {
                    CategoryId = id,
                    Categories = context.Categories,
                    Products = context.Products.Where(x => x.CategoryId == id).OrderByDescending(x => x.Id).Skip(parperntPage * (page - 1)).Take(parperntPage),
                };
                count = result.Products.Count();
                result.Count = count;

                return PartialView("_CardPartial", result);

            }

            var items = new IndexModel
            {
                Products = context.Products.OrderByDescending(x => x.Id).Skip(parperntPage * (page -1)).Take(parperntPage),
                Categories = null
            };
            count = items.Products.Count();
            items.Count = count;

            return PartialView("_CardPartial",items);
        }



        public IActionResult DeleteInCart(int id)
        {

            cartService.Remove(id);
            return RedirectToAction("Index", "Home");

        }

        public IActionResult InfoProduct(int id)
        {
            var result = context.Products.FirstOrDefault(x => x.Id == id);
            return View(result);

        }

        public IActionResult Search(string title)
        {
            var results = context.Products.Where(x => x.Title == title);
            return View(results);

        }

        public IActionResult ClearAllInCart()
        {

            cartService.Clear();
            return RedirectToAction("Index", "Home");
        }



        /// </ENDDD>

        public IActionResult AddToCard(int id,string returnUrl)
        {

            cartService.Add(id);

            if (returnUrl == null)
                return RedirectToAction("Index", "Home");
            else
                return Redirect(returnUrl);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
