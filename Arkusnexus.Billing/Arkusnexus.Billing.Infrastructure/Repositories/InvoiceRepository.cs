using Arkusnexus.Billing.Domain.Entities;
using Arkusnexus.Billing.Infrastructure.Repositories.Abstractions;

namespace Arkusnexus.Billing.Infrastructure.Repositories
{
    public class InvoiceRepository : BillingEntityRepository<Invoice>, IInvoiceRepository
    {
        public InvoiceRepository(BillingContext context) : base(context)
        {

        }
    }
}
