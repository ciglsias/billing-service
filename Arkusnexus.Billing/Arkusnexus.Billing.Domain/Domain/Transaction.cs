using Arkusnexus.Billing.Domain.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arkusnexus.Billing.Domain.Domain
{
    public class Transaction : BillingEntity
    {
        public DateTime DateTime { get; set; }

        public double Ammout { get; set; }

        public string Description { get; set; } = "";

        public BillingStatus BillingStatus { get; set; } = BillingStatus.Unbilled;
    }
}
