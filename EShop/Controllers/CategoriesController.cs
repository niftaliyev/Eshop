using EShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Controllers
{
    public class CategoriesController : Controller
    {


        private readonly EShopDbContext context;

        public CategoriesController(EShopDbContext context)
        {

            this.context = context;
        }


        public IActionResult Index()
        {
            return View();
        }





        public IActionResult Category(int id)
        {
            //        var order = await _context.Orders
            //.Include(x => x.OrderProducts)
            //.ThenInclude(x => x.Product)
            //.FirstOrDefaultAsync(m => m.Id == id);


            var result = context.Products.Where(x => x.Id == id);

            return View(result);
        }
    }
}
