using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Commands.UpdateOrder
{
    public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
    {
        public UpdateOrderCommandValidator()
        {

            RuleFor(p => p.UserName)
                .NotEmpty().WithMessage("username is required")
                .NotNull()
                .MaximumLength(50).WithMessage("username must be less than 50 characters");


            RuleFor(p => p.EmailAddress)
                .NotEmpty().WithMessage("Email is required");

            RuleFor(p => p.TotalPrice)
                .NotEmpty().WithMessage("TotalPrice can't be empty")
                .GreaterThan(50).WithMessage("TotalPrice can't be equal to 0");
        }
    }
}
