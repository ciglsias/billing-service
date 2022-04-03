using Arkusnexus.Billing.Domain.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public bool DeleteById(int id)
        {
            var entity = _dbSet.FirstOrDefault(x => x.Id == id);

            if (entity != null)
            {
                _dbSet.Remove(entity);

                return true;
            }

            return false;
        }

        public IQueryable<T> GetAll()
        {
            return _dbSet.AsQueryable();
        }

        public T GetById(int id)
        {
            return _dbSet.FirstOrDefault(x => x.Id == id);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public T Update(T entity)
        {
            return _dbSet.Update(entity).Entity;
        }
    }
}
