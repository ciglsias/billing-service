using Arkusnexus.Billing.Infrastructure.Repositories;
using Arkusnexus.Billing.Infrastructure.Repositories.Abstractions;

namespace Arkusnexus.Billing.Infrastructure
{
    public class BillingUnitOfWork : IBillingUnitOfWork
    {
        //context to reuse between Repositories
        readonly BillingContext _context;

        public BillingUnitOfWork()
        {
            _context = new BillingContext();

            TransactionRepository = new TransactionRepository(_context);

            InvoiceRepository = new InvoiceRepository(_context);
        }
        public ITransactionRepository TransactionRepository { get; }

        public IInvoiceRepository InvoiceRepository { get; }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
