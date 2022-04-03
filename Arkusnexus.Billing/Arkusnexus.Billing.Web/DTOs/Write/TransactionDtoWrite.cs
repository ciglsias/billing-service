using Arkusnexus.Billing.Domain.Entities;

namespace Arkusnexus.Billing.Web.DTOs.Write
{
    public class TransactionDtoWrite
    {
        public double Ammout { get; set; }

        public string Description { get; set; } = "";

        public BillingStatus BillingStatus { get; set; } = BillingStatus.Unbilled;
    }
}
