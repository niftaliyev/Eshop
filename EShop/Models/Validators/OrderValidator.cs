using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Models.Validators
{
    public class OrderValidator : AbstractValidator<Order>
    {
        public OrderValidator()
        {
            RuleFor(x => x.FullName).Length(2, 100).WithMessage("2 den az 100 den cox olmasin").NotNull().WithMessage("bos olmaz");
            RuleFor(x => x.Email).NotNull().EmailAddress();
            RuleFor(x => x.Phone).NotNull();
            RuleFor(x => x.Address).NotNull();
        }
    }
}
