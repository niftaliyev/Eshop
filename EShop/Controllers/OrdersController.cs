﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EShop.Models;
using EShop.Service;
using System.Text;

namespace EShop.Controllers
{
    public class OrdersController : Controller
    {
        private readonly EShopDbContext _context;
        private readonly ICartServiceTwo cartService;
        private readonly IEmailSenderTwo emailSender;

        public OrdersController(EShopDbContext context , ICartServiceTwo cartService , IEmailSenderTwo emailSender)
        {
            _context = context;
            this.cartService = cartService;
            this.emailSender = emailSender;
        }







        


        // GET: Orders/Create
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FullName,Email,Phone,Address,Comment,TotalPrice,OrderStatus")] Order order)
        {
            if (ModelState.IsValid)
            {
                var cartItems = cartService.GetProducts();
                order.TotalPrice = cartItems.Sum(x => x.Product.Price * x.Amount);
                _context.Add(order);
                await _context.SaveChangesAsync();


                var orderProducts = cartItems.Select(x => new OrderProduct 
                {
                    OrderId = order.Id,
                    ProductId = x.Product.Id,
                    Amount = x.Amount
                });

                _context.OrderProducts.AddRange(orderProducts);
                await _context.SaveChangesAsync();

                cartService.Clear();

                StringBuilder content = new StringBuilder(1000);
                foreach (var item in cartItems)
                {
                    content.Append($"{item.Product.Title} {item.Amount}<br>");
                }
                await emailSender.SendEmailAsync(order.Email, "Order from EShop", content.ToString());

                return RedirectToAction("Index","Home");


            }
            return View(order);
        }

       
    }
}
