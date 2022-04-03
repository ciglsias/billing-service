using Arkusnexus.Billing.Domain.Entities.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Arkusnexus.Billing.Infrastructure.Repositories.Abstractions
{
    public class BillingEntityRepository<T> : IBillingEntityRepository<T> where T : BillingEntity
    {
        private readonly BillingContext _context;

        private readonly DbSet<T> _dbSet;

        public BillingEntityRepository(BillingContext context)
        {
            _context = context;

            _dbSet = context.Set<T>();
        }

        public T Add(T entity)
        {
            return _dbSet.Add(entity).Entity;
        }

        public async Task<bool> DeleteById(int id)
        {
            var entity = await _dbSet.FindAsync(id);

            if (entity != null)
            {
                _dbSet.Remove(entity);

                await _context.SaveChangesAsync();

                return true;
            }

            return false;
        }

        public IQueryable<T> GetAll()
        {
            return _dbSet.AsQueryable();
        }

        public async Task<T?> GetById(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<T> Update(T entity)
        {
            var entityFound = await _dbSet.FindAsync(entity.Id);

            if (entityFound == null)
            {
                return null;
            }
            else
            {
                //todo: manage concurrency better

                _dbSet.Remove(entityFound);

                var added = _dbSet.Add(entity);

                return added.Entity;
            }
        }
    }
}
