using FluentValidation;

namespace Ordering.Application.Features.Orders.Queries.GetOrdersList
{
    public class GetOrdersListQueryValidator: AbstractValidator<GetOrdersListQuery>
    {
        public GetOrdersListQueryValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty()
                .MinimumLength(4)
                .WithMessage("el username no puede ser menor que 4 caracteres")
                .MaximumLength(20);
        }

    }
}
