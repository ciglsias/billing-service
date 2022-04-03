using Arkusnexus.Billing.Domain.Entities.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arkusnexus.Billing.Infrastructure.Repositories.Abstractions
{
    public interface IBillingEntityRepository<T> where T : BillingEntity
    {
        T Add(T entity);

        T Update(T entity);

        Task<bool> DeleteById(int id);

        Task<T?> GetById(int id);

        IQueryable<T> GetAll();

        Task SaveChangesAsync();
    }
}
