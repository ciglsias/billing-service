using Arkusnexus.Billing.Infrastructure.Repositories;
using Arkusnexus.Billing.Infrastructure.Repositories.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arkusnexus.Billing.Infrastructure
{
    public interface IBillingUnitOfWork
    {
        ITransactionRepository TransactionRepository { get; }

        IInvoiceRepository InvoiceRepository { get; }

        Task SaveChangesAsync();
    }
}
