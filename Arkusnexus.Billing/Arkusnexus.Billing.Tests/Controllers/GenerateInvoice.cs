using Arkusnexus.Billing.Infrastructure;
using Arkusnexus.Billing.Infrastructure.Repositories.Abstractions;
using Arkusnexus.Billing.Web.Controllers;
using Arkusnexus.Billing.Web.Mapping;
using AutoFixture;
using AutoMapper;
using MockQueryable.Moq;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Arkusnexus.Billing.Tests.Controllers
{
    public class GenerateInvoice
    {
        private readonly IMapper _mapper = new MapperConfiguration(cfg => cfg.AddProfile<BillingAutoMapperProfile>()).CreateMapper();

        [Fact]
        public async Task GenerateInvoice_ShouldMarkBilled()
        {
            //arrange: mock transaction repository
            Mock<ITransactionRepository>? transactionsRrepositoryMock = new Mock<ITransactionRepository>();

            List<Domain.Entities.Transaction>? transactions = new List<Domain.Entities.Transaction>()
            {
                FixtureUnbilledTransaction(),
                FixtureUnbilledTransaction(),
                FixtureUnbilledTransaction(),
                FixtureUnbilledTransaction(),
                FixtureUnbilledTransaction(),
            };

            transactions.Sort(new Comparison<Domain.Entities.Transaction>((x, y) => Comparer<DateTime>.Default.Compare(x.DateTime, y.DateTime)));

            IQueryable<Domain.Entities.Transaction>? dataQueryableMock = transactions.AsQueryable().BuildMock();

            transactionsRrepositoryMock.Setup(x => x.GetAll()).Returns(dataQueryableMock);

            //arrange: mock invoices repository
            Mock<IInvoiceRepository>? invoicesRepositoryMock = new Mock<IInvoiceRepository>();

            invoicesRepositoryMock.Setup(x => x.Add(It.IsAny<Domain.Entities.Invoice>())).Verifiable();

            //arrange: mock unit of work
            Mock<IBillingUnitOfWork>? unitOfWorkMock = new Mock<IBillingUnitOfWork>();

            unitOfWorkMock.SetupGet(x => x.TransactionRepository).Returns(transactionsRrepositoryMock.Object);
            
            unitOfWorkMock.SetupGet(x => x.InvoiceRepository).Returns(invoicesRepositoryMock.Object);

            unitOfWorkMock.Setup(x => x.SaveChangesAsync()).Verifiable();

            //arrange: controller created for act
            TransactionsAdvancedController? controller = new TransactionsAdvancedController(unitOfWorkMock.Object, _mapper);

            //act
            Microsoft.AspNetCore.Mvc.IActionResult? invoiceResult = await controller.GenerateInvoice(transactions[1].DateTime, transactions[3].DateTime);

            //asserts
            Microsoft.AspNetCore.Mvc.CreatedAtActionResult? createdResult = Assert.IsType<Microsoft.AspNetCore.Mvc.CreatedAtActionResult>(invoiceResult);

            Web.DTOs.Read.InvoiceDtoRead? returnValue = Assert.IsType<Web.DTOs.Read.InvoiceDtoRead>(createdResult.Value);

            Assert.Equal(3, returnValue.Transactions.Count);

            Assert.True(returnValue.Transactions.All(x => x.BillingStatus == Domain.Entities.BillingStatus.Billed));

            Assert.True(transactions[0].BillingStatus == Domain.Entities.BillingStatus.Unbilled);

            Assert.True(transactions[4].BillingStatus == Domain.Entities.BillingStatus.Unbilled);

            invoicesRepositoryMock.Verify();

            unitOfWorkMock.Verify();
        }

        private Domain.Entities.Transaction FixtureUnbilledTransaction()
        {
            Fixture? fixture = new Fixture();

            Domain.Entities.Transaction? result = fixture.Create<Domain.Entities.Transaction>();

            result.BillingStatus = Domain.Entities.BillingStatus.Unbilled;

            return result;
        }
    }
}
