using System.Linq.Expressions;

namespace ElectronicsShop.DAL.IRepositories
{
    public interface IGenericRepository<T> where T : class
    {
        IQueryable<T> All { get; }
        IQueryable<T> GetAll();
        IQueryable<T> Where(Expression<Func<T, bool>> predicate);
        void Add(T entity);
        int Count();
    }
}
