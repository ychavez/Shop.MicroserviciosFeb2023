using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ordering.Application.Features.Orders.Commands.Checkout;
using Ordering.Application.Features.Orders.Queries.GetOrdersList;

namespace Ordering.api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class OrderController: ControllerBase
    {
        private readonly IMediator mediator;

        public OrderController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("{userName}")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<ActionResult<IEnumerable<OrdersViewModel>>> GetOrders(string userName)
            => await mediator.Send(new GetOrdersListQuery { UserName = userName });

        [HttpPost]
        public async Task<ActionResult<int>> CreateOrder([FromBody] CheckoutCommand command)
            => await mediator.Send(command);
    }
}
