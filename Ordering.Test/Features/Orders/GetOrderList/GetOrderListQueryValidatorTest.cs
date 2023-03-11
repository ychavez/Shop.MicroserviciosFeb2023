using Ordering.Application.Features.Orders.Queries.GetOrdersList;

namespace Ordering.Test.Features.Orders.GetOrderList
{
    public class GetOrderListQueryValidatorTest
    {

        public readonly GetOrdersListQueryValidator validations;

        public GetOrderListQueryValidatorTest()
        {
            validations = new GetOrdersListQueryValidator();
        }

        [Theory]
        [InlineData("as")]
        [InlineData("aasdfghjkloiuytrewqas")]
        public void GetOrderListQuery_UserName_ShouldHaveError(string userName) 
        {
            //Arrange
            var query = new GetOrdersListQuery() { UserName = userName };

            //Act
            var result = validations.Validate(query);

            //Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, x => x.PropertyName == nameof(GetOrdersListQuery.UserName));

        
        }


    }
}
