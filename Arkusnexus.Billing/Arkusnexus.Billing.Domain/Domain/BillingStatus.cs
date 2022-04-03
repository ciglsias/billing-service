using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arkusnexus.Billing.Domain.Domain
{
    public enum BillingStatus
    {
        Unbilled = 0,
        Billed = 1,
        Paid = 2,
    }
}
