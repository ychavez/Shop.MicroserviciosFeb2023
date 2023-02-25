using Ordering.Domain.Common;

namespace Ordering.Domain.Entities
{
    public class Order: EntityBase, IMultiTenant
    {
        public string UserName { get; set; } = null!;
        public decimal TotalPrice { get; set; }

        //
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Address { get; set; } = null!;
        public int PaymentMethod { get; set; }
        public Guid TenantId { get; set; }
    }
}
