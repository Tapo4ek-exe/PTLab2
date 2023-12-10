namespace EShop.Infrastructure
{
    public class DbInitializer
    {
        public static void Initialize(EShopDbContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}
