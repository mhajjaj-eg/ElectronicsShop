using ElectronicsShop.DAL.Data;
using ElectronicsShop.DAL.Repositories;
using ElectronicsShop.Models;
using Microsoft.EntityFrameworkCore;

namespace ElectronicsShop.DAL.IRepositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationContext dbContext)
            : base(dbContext)
        { }

        public List<Product> GetProductsWithPaging(int pageNum, int countPerPage, int typeId = 0)
        {
            return GetListByType(typeId)
                .Skip((pageNum - 1) * countPerPage)
                .Take(countPerPage)
                .Include(a => a.ProductCategory)
                .ToList();
        }

        public int GetGridCount(int typeId)
        {
            return GetListByType(typeId).Count();
        }

        private IQueryable<Product> GetListByType(int typeId)
        {
            if (typeId != 0)
                return Where(a => a.ProductCategory.ProductCategoryId == typeId);
            return All;
        }

        public int GetQuantityInStock(int ProductId)
        {
            return Where(a => a.ProductId == ProductId).FirstOrDefault().QuantityInStock;
        }
    }
}
