using Arkusnexus.Billing.Infrastructure;
using Arkusnexus.Billing.Infrastructure.Repositories.Abstractions;
using Arkusnexus.Billing.Web.Controllers;
using Arkusnexus.Billing.Web.Mapping;
using AutoFixture;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MockQueryable.Moq;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Arkusnexus.Billing.Tests.Controllers
{
    public class GenerateInvoice
    {
        private readonly IMapper _mapper = new MapperConfiguration(cfg => cfg.AddProfile<BillingAutoMapperProfile>()).CreateMapper();

        async IAsyncEnumerable<T> GetAsyncEnumerable<T>(List<T> list)
        {
            foreach (var item in list)
            {
                yield return item;
            }
        }

        [Fact]
        public async Task GenerateInvoice_ShouldMarkBilled()
        {
            //arrange
            var repositoryMock = new Mock<ITransactionRepository>();

            var transactions = new List<Domain.Entities.Transaction>() 
            {
                FixtureUnbilledTransaction(),
                FixtureUnbilledTransaction(),
                FixtureUnbilledTransaction(),
                FixtureUnbilledTransaction(),
                FixtureUnbilledTransaction(),
            };

            transactions.Sort(new Comparison<Domain.Entities.Transaction>((x, y) => Comparer<DateTime>.Default.Compare(x.DateTime, y.DateTime)));

            var dataQueryableMock = transactions.AsQueryable().BuildMock();

            repositoryMock.Setup(x => x.GetAll()).Returns(dataQueryableMock);

            var unitOfWorkMock = new Mock<IBillingUnitOfWork>();

            unitOfWorkMock.SetupGet(x => x.TransactionRepository).Returns(repositoryMock.Object);

            unitOfWorkMock.Setup(x => x.SaveChangesAsync()).Verifiable();

            var controller = new TransactionsAdvancedController(unitOfWorkMock.Object, _mapper);

            //act
            var invoiceResult = await controller.GenerateInvoice(transactions[1].DateTime, transactions[3].DateTime);

            //asserts
            var createdResult = Assert.IsType<Microsoft.AspNetCore.Mvc.CreatedAtActionResult>(invoiceResult);

            var returnValue = Assert.IsType<List<Web.DTOs.Read.TransactionDtoRead>>(createdResult.Value);

            Assert.Equal(3, returnValue.Count);

            Assert.True(returnValue.All(x => x.BillingStatus == Domain.Entities.BillingStatus.Billed));

            Assert.True(transactions[0].BillingStatus == Domain.Entities.BillingStatus.Unbilled);

            Assert.True(transactions[4].BillingStatus == Domain.Entities.BillingStatus.Unbilled);

            repositoryMock.Verify();

            unitOfWorkMock.Verify();
        }

        private Domain.Entities.Transaction FixtureUnbilledTransaction()
        {
            var fixture = new Fixture();

            var result = fixture.Create<Domain.Entities.Transaction>();

            result.BillingStatus = Domain.Entities.BillingStatus.Unbilled;

            return result;
        }
    }
}
