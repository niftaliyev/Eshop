using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login(string ReturnUrl=null)
        {
            return PartialView("_AdminLoginViewPartial",ReturnUrl);
        }
    }
}
