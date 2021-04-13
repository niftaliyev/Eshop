using EShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Service
{
    public interface ICartServiceTwo
    {
        void Add(int id);
        void Remove(int id);
        IEnumerable<CartItemViewModel> GetProducts();
        void Clear();
    }
}
