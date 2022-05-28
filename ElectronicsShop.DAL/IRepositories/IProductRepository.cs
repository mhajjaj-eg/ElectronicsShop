using ElectronicsShop.Models;

namespace ElectronicsShop.DAL.IRepositories
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        public List<Product> GetProductsWithPaging(int pageNum, int countPerPage, int typeId = 0);
        int GetGridCount(int typeId);
        int GetQuantityInStock(int ProductId);
    }
}
