namespace EShop.Domain
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Guid SaleId { get; set; }
        public Sale Sale { get; set; }
        public IEnumerable<Purchase> Purchases { get; set; }
    }
}
