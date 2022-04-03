using Arkusnexus.Billing.Domain.Entities.Abstractions;

namespace Arkusnexus.Billing.Domain.Entities
{
    public class Invoice : BillingEntity
    {
        public DateTime DateTime { get; set; }

        public IList<Transaction> Transactions { get; set; } = null!;

        public bool Paid { get; set; }
    }
}
