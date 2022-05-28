using ElectronicsShop.DAL.Data;
using ElectronicsShop.DAL.Repositories;
using ElectronicsShop.Models;
using Microsoft.EntityFrameworkCore;

namespace ElectronicsShop.DAL.IRepositories
{
    public class ProductOrderRepository : GenericRepository<ProductOrder>, IProductOrderRepository
    {
        public ProductOrderRepository(ApplicationContext dbContext)
            : base(dbContext)
        { }

        public List<ProductOrder> GetProductOrdersWithPaging(int pageNum, int countPerPage, int? typeId = null)
        {
            return All.Where(p => p.Product.ProductCategoryId == typeId || typeId == null)
            .Skip((pageNum - 1) * countPerPage)
            .Take(countPerPage)
            .Include(a => a.Client)
            .Include(a => a.Product)
            .ToList();
        }
    }
}
