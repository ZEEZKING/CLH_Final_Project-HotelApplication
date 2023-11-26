using CLH_Final_Project.Entities.Context;
using CLH_Final_Project.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CLH_Final_Project.Implementation.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class, new()
    {
        protected ApplicationDbContext _Context;
        public async Task<T> CreateAsync(T entity)
        {
            await _Context.Set<T>().AddAsync(entity);
            await _Context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(T entity)
        {
            _Context.Set<T>().Remove(entity);
            await _Context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(Expression<Func<T, bool>> expression)
        {
            return await _Context.Set<T>().AnyAsync(expression);    
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _Context.Set<T>().ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> expression)
        {
           return await _Context.Set<T>().Where(expression).ToListAsync();  
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> expression)
        {
            return await _Context.Set<T>().SingleOrDefaultAsync(expression);
        }

        public async Task<T> GetAsync(int id)
        {
            return await _Context.Set<T>().FindAsync(id);
        }

        public  async Task SaveChangesAsync()
        {
            await _Context.SaveChangesAsync();
        }

        public async Task<T> UpdateAsync(T entity)
        {
            await _Context.SaveChangesAsync();
            return entity;
        }
    }
}
