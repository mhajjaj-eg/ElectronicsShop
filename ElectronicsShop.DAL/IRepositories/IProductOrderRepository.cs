using ElectronicsShop.Models;

namespace ElectronicsShop.DAL.IRepositories
{
    public interface IProductOrderRepository : IGenericRepository<ProductOrder>
    {
        public List<ProductOrder> GetProductOrdersWithPaging(int pageNum, int countPerPage, int? typeId = null);
    }
}
