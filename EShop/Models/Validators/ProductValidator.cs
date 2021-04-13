using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Models.Validators
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(x => x.Title).NotNull().WithMessage("null nelzya").Length(2,100).WithMessage("2 ve ya 100 simvol");
            RuleFor(x => x.Price).NotNull().WithMessage("qiymeti  yaze");
        }
    }
}
