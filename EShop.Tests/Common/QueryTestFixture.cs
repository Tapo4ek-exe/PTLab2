using AutoMapper;
using EShop.Application.Common.Mappings;
using EShop.Application.Interfaces;
using EShop.Infrastructure;

namespace EShop.Tests.Common
{
    public class QueryTestFixture : IDisposable
    {
        public EShopDbContext Context;
        public IMapper Mapper;

        public QueryTestFixture()
        {
            Context = EShopDbContextFactory.Create();
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AssemblyMappingProfile(typeof(IEShopDbContext).Assembly));
            });
            Mapper = configurationProvider.CreateMapper();
        }

        public void Dispose()
        {
            EShopDbContextFactory.Destroy(Context);
        }

        [CollectionDefinition("QueryCollection")]
        public class QueryCollection : ICollectionFixture<QueryTestFixture> { }
    }
}
