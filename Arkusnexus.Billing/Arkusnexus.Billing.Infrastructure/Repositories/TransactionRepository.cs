﻿using Arkusnexus.Billing.Domain.Domain;
using Arkusnexus.Billing.Infrastructure.Repositories.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arkusnexus.Billing.Infrastructure.Repositories
{
    public class TransactionRepository : EntityRepository<Transaction>, ITransactionRepository
    {
    }
}
