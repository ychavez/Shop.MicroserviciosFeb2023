using Grpc.Core;
using Inventory.grpc.Protos;

namespace Inventory.grpc.Services
{
    public class ProductService : ExistanceService.ExistanceServiceBase
    {

        public override async Task<ProductExistanceReply> CheckExistance(ProductRequest request, 
            ServerCallContext context)
        {
            return new ProductExistanceReply { ProductQty = 99 };
        }
    }
}
