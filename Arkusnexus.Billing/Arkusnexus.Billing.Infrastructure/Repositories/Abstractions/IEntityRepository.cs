using Arkusnexus.Billing.Domain.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arkusnexus.Billing.Infrastructure.Repositories.Abstractions
{
    public interface IEntityRepository<T> where T : BillingEntity
    {
        T Add(T entity);

        T Update(T entity);

        bool DeleteById(int id);

        T GetById(int id);

        IQueryable<T> GetAll();
    }
}
