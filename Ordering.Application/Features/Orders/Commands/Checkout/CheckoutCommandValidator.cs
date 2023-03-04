using FluentValidation;

namespace Ordering.Application.Features.Orders.Commands.Checkout
{
    public class CheckoutCommandValidator : AbstractValidator<CheckoutCommand>
    {
        public CheckoutCommandValidator()
        {
            RuleFor(x => x.Address)
             .EmailAddress()
             .NotEmpty();

            RuleFor(x => x.TotalPrice)
                .GreaterThan(0);

            RuleFor(x => x.UserName)
                .MinimumLength(4)
                .MaximumLength(20);
        }
    }
}
