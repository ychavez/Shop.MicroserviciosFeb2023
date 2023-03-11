using AutoMapper;
using Moq;
using Ordering.Application.Contracts;
using Ordering.Application.Features.Orders.Commands.Checkout;
using Ordering.Domain.Entities;

namespace Ordering.Test.Features.Orders.Checkout
{

    public class CheckoutCommandHandlerTest
    {
        private readonly Mock<IGenericRepository<Order>> _repositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly CheckoutCommandHandler _handler;

        public CheckoutCommandHandlerTest()
        {
            _repositoryMock = new Mock<IGenericRepository<Order>>();
            _mapperMock = new Mock<IMapper>();
            _handler = new CheckoutCommandHandler(_repositoryMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldAddNewOrderToRepository() 
        {
            //Arrange
            var checkoutOrderCommand = new CheckoutCommand()
            {
                UserName = "testUser",
                TotalPrice = 10.5m,
                FirstName = "John",
                LastName = "Doe",
                Address = "123 main st",
                PaymentMethod = 1
            };
            
            var orderEntity = new Order() {Id = 12 };

            _mapperMock.Setup(x => x.Map<Order>(checkoutOrderCommand)).Returns(orderEntity);
            _repositoryMock.Setup(x => x.AddAsync(orderEntity)).ReturnsAsync(orderEntity);


            //Act
            var result = await _handler.Handle(checkoutOrderCommand, CancellationToken.None);


            //Assert
            _repositoryMock.Verify(r => r.AddAsync(orderEntity), Times.Once);
            _mapperMock.Verify(m => m.Map<Order>(checkoutOrderCommand), Times.Once);
            Assert.Equal(result, orderEntity.Id);
        
        }

    }
}
