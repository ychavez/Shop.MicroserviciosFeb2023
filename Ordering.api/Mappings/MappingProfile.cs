using AutoMapper;
using EventBus.Messages.Events;
using Ordering.Application.Features.Orders.Commands.Checkout;

namespace Ordering.api.Mappings
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<CheckoutCommand, BasketCheckoutEvent>().ReverseMap();
        }
    }
}
