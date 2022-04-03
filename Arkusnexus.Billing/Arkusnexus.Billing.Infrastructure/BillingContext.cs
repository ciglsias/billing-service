using Arkusnexus.Billing.Domain.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
