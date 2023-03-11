﻿using AutoMapper;
using MediatR;
using Ordering.Application.Contracts;
using Ordering.Domain.Entities;

namespace Ordering.Application.Features.Orders.Commands.Checkout
{
    public class CheckoutCommandHandler : IRequestHandler<CheckoutCommand, int>
    {
        private readonly IGenericRepository<Order> repository;
        private readonly IMapper mapper;

        public CheckoutCommandHandler(IGenericRepository<Order> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<int> Handle(CheckoutCommand request, CancellationToken cancellationToken)
        {
            var orderEntity = mapper.Map<Order>(request);
            var newOrder = await repository.AddAsync(orderEntity);
            return newOrder.Id;
        }
    }
}
