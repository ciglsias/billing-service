using Arkusnexus.Billing.Domain.Entities.Abstractions;

namespace Arkusnexus.Billing.Infrastructure.Repositories.Abstractions
{
    public interface IBillingEntityRepository<T> where T : BillingEntity
    {
        T Add(T entity);

        Task<T> Update(T entity);

        Task<bool> DeleteById(int id);

        Task<T?> GetById(int id);

        IQueryable<T> GetAll();

        Task SaveChangesAsync();
    }
}
