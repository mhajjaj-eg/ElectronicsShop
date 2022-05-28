using ElectronicsShop.DAL.Data;
using ElectronicsShop.DAL.Repositories;
using ElectronicsShop.Models;

namespace ElectronicsShop.DAL.IRepositories
{
    public class ProductCategoryRepository : GenericRepository<ProductCategory>, IProductCategoryRepository
    {
        public ProductCategoryRepository(ApplicationContext dbContext)
            : base(dbContext)
        { }
    }
}
