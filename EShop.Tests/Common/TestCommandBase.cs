using EShop.Infrastructure;

namespace EShop.Tests.Common
{
    public abstract class TestCommandBase : IDisposable
    {
        protected readonly EShopDbContext Context;

        public TestCommandBase()
        {
            Context = EShopDbContextFactory.Create();
        }

        public void Dispose()
        {
            EShopDbContextFactory.Destroy(Context);
        }
    }
}
