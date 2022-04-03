using Arkusnexus.Billing.Domain.Entities;

namespace Arkusnexus.Billing.Infrastructure.Repositories.Abstractions
{
    public interface ITransactionRepository : IBillingEntityRepository<Transaction>
    {
    }
}
