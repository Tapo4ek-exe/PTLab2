namespace EShop.Domain
{
    public class Purchase
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public string Address { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public double UsedPrice { get; set; }
    }
}
