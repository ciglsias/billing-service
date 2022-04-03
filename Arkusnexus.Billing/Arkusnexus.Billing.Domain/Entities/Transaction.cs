using Arkusnexus.Billing.Domain.Entities.Abstractions;

namespace Arkusnexus.Billing.Domain.Entities
{
    public class Transaction : BillingEntity
    {
        public DateTime DateTime { get; set; }

        public double Ammout { get; set; }

        public string Description { get; set; } = "";

        public BillingStatus BillingStatus { get; set; } = BillingStatus.Unbilled;
    }
}
