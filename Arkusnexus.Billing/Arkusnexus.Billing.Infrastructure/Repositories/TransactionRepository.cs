using Arkusnexus.Billing.Domain.Entities;
using Arkusnexus.Billing.Infrastructure.Repositories.Abstractions;

namespace Arkusnexus.Billing.Infrastructure.Repositories
{
    public class TransactionRepository : BillingEntityRepository<Transaction>, ITransactionRepository
    {
        public TransactionRepository(BillingContext context) : base(context)
        {

        }
    }
}
