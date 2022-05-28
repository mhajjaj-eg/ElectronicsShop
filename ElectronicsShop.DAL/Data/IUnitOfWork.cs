
using ElectronicsShop.DAL.IRepositories;

namespace ElectronicsShop.DAL.Data
{
    public interface IUnitOfWork
{
    IProductRepository ProductRepository { get; }
    IProductCategoryRepository ProductCategoryRepository { get; }
    IProductOrderRepository ProductOrderRepository { get; }
    void SaveChanges();
}
}
