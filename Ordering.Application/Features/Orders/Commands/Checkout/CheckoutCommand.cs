using MediatR;

namespace Ordering.Application.Features.Orders.Commands.Checkout
{
    public class CheckoutCommand: IRequest<int>
    {
        public string UserName { get; set; } = null!;
        public decimal TotalPrice { get; set; }

        //
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Address { get; set; } = null!;
        public int PaymentMethod { get; set; }
    }
}
