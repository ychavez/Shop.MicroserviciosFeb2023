namespace Basket.api.Entities
{
    public class BasketCheckout
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
