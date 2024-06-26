﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Models.Identity
{

    public class AppUser : IdentityUser
    {
        public string Email { get; set; }

        public IEnumerable<Car> Cars { get; set; }

    }

}
