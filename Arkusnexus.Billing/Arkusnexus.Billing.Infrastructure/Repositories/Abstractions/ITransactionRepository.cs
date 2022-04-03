using Arkusnexus.Billing.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arkusnexus.Billing.Infrastructure.Repositories.Abstractions
{
    public interface ITransactionRepository : IBillingEntityRepository<Transaction>
    {
    }
}
