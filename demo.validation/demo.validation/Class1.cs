using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace demo.validation
{
    public class Class1
    {
        public void X()
        {
            var o = new Order();
            var v = new OrderValidator();
            var r = v.Validate(o);

            var f = r.Errors;
        }
    }

    public class OrderValidator : AbstractValidator<Order>
    {
        public OrderValidator()
        {
            RuleFor(x => x.Uom)
                .NotEmpty()
                .NotNull()
                .WithMessage("You must provide a UOM");
        }
    }

    public class Order
    {
        public string Uom { get; set; }
    }
}
