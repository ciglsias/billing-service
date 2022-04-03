using Arkusnexus.Billing.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arkusnexus.Billing.Web.DTOs.Read
{
    public class TransactionDtoRead
    {
        public int Id { get; set; }

        public DateTime DateTime { get; set; }

        public double Ammout { get; set; }

        public string Description { get; set; } = "";

        public BillingStatus BillingStatus { get; set; } = BillingStatus.Unbilled;
    }
}
