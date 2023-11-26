using System.Linq.Expressions;

namespace CLH_Final_Project.Interfaces.Repositories
{
    public interface IBaseRepository<T>
    {
        Task<T> CreateAsync(T entity);
        Task SaveChangesAsync();
        Task<bool> DeleteAsync(T entity);
        Task<bool> ExistsAsync(Expression<Func<T, bool>> expression);
        Task<List<T>> GetAllAsync(); 
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> expression);
        Task<T> GetAsync(Expression<Func<T, bool>> expression);
        Task<T> GetAsync(int id);
        Task<T> UpdateAsync(T entity);
    }
}
