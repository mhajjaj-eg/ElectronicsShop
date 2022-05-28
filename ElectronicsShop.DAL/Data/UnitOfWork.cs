using ElectronicsShop.DAL.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace ElectronicsShop.DAL.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext _context;

        public IProductRepository _productRepository { get; }
        public IProductCategoryRepository _productCategoryRepository { get; }
        public IProductOrderRepository _productOrderRepository { get; }
        public IProductRepository ProductRepository => _productRepository;
        public IProductCategoryRepository ProductCategoryRepository => _productCategoryRepository;
        public IProductOrderRepository ProductOrderRepository => _productOrderRepository;
        public UnitOfWork(
            ApplicationContext context,
            IProductRepository productRepository,
            IProductCategoryRepository ProductCategoryRepository,
            IProductOrderRepository ProductOrderRepository)
        {
            _context = context;
            _productRepository = productRepository;
            _productCategoryRepository = ProductCategoryRepository;
            _productOrderRepository = ProductOrderRepository;
        }
        public void SaveChanges()
        {
            bool saveFailed;
            do
            {
                saveFailed = false;

                try
                {
                    _context.SaveChanges();
                }
                catch (DbUpdateException ex)
                {
                    saveFailed = true;
                    ex.Entries.Single().Reload();
                }

            } while (saveFailed);
        }
    }
}
