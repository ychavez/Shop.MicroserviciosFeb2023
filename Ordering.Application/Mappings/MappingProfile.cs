﻿using AutoMapper;
using Ordering.Application.Features.Orders.Commands.Checkout;
using Ordering.Application.Features.Orders.Queries.GetOrdersList;
using Ordering.Domain.Entities;

namespace Ordering.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Order, OrdersViewModel>().ReverseMap();

            CreateMap<Order, CheckoutCommand>().ReverseMap();
        }
    }
}
