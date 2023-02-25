using Ordering.Domain.Common;
using Ordering.Infrastructure.Persistence;

namespace Ordering.Infrastructure.Repositories
{
    public class GenericRepository<T> where T : EntityBase
    {
        private readonly OrderContext orderContext;

        public GenericRepository(OrderContext orderContext)
        {
            this.orderContext = orderContext;
        }
    }
}
