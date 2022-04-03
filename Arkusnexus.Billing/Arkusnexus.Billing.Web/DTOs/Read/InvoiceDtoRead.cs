using Arkusnexus.Billing.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arkusnexus.Billing.Web.DTOs.Read
{
    public class InvoiceDtoRead
    {
        public int Id { get; set; }

        public DateTime DateTime { get; set; }

        public bool Paid { get; set; }

        public List<TransactionDtoRead> Transactions { get; set; } = null!;
    }
}
