using Arkusnexus.Billing.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Arkusnexus.Billing.Infrastructure
{
    public class BillingContext : DbContext
    {
        public BillingContext() : base(new DbContextOptionsBuilder<BillingContext>()
            .UseInMemoryDatabase("BloggingControllerTest")
            .Options)
        {
        }

        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
    }
}
