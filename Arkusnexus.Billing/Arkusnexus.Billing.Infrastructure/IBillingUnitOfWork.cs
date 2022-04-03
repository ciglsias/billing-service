using Arkusnexus.Billing.Infrastructure.Repositories.Abstractions;

namespace Arkusnexus.Billing.Infrastructure
{
    public interface IBillingUnitOfWork
    {
        ITransactionRepository TransactionRepository { get; }

        IInvoiceRepository InvoiceRepository { get; }

        Task SaveChangesAsync();
    }
}
