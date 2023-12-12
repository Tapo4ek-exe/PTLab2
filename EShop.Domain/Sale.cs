namespace EShop.Domain
{
    public class Sale
    {
        public Guid Id { get; set; }
        public double Value { get; set; }
        public User User { get; set; }
    }
}
