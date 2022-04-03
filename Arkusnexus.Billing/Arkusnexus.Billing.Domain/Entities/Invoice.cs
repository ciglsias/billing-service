using Arkusnexus.Billing.Domain.Entities.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arkusnexus.Billing.Domain.Entities
{
    public class Invoice : BillingEntity
    {
        public DateTime DateTime { get; set; }

        public IList<Transaction> Transactions { get; set; } = null!;

        public bool Paid { get; set; }
    }
}
