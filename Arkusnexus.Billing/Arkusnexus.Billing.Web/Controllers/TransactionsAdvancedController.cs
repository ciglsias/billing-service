using Arkusnexus.Billing.Domain.Entities;
using Arkusnexus.Billing.Infrastructure;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Arkusnexus.Billing.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransactionsAdvancedController : ControllerBase
    {
        readonly IBillingUnitOfWork _unitOfWork;

        readonly IMapper _mapper;

        public TransactionsAdvancedController(IBillingUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;

            _mapper = mapper;
        }

        [HttpPost("GenerateInvoice")]
        public async Task<IActionResult> GenerateInvoice(DateTime from, DateTime to)
        {
            var transactions = await _unitOfWork
                .TransactionRepository
                .GetAll()
                .Where(t => t.DateTime >= from && t.DateTime <= to && t.BillingStatus == BillingStatus.Unbilled)
                .ToListAsync()
                ;

            if (transactions.Count == 0)
            {
                return NotFound();
            }

            foreach (var transaction in transactions)
            {
                transaction.BillingStatus = BillingStatus.Billed;
            }

            await _unitOfWork.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GenerateInvoice),
                transactions.Select(t => _mapper.Map<DTOs.Read.TransactionDtoRead>(t)));
        }

        [HttpPut("SetTransactionAsPaid")]
        public async Task<IActionResult> SetTransactionAsPaid(int transactionId)
        {
            return await SetBillingStatus(transactionId, BillingStatus.Paid);
        }

        [HttpPut("SetTransactionAsBilled")]
        public async Task<IActionResult> SetTransactionAsBilled(int transactionId)
        {
            return await SetBillingStatus(transactionId, BillingStatus.Billed);
        }

        [HttpPut("SetTransactionAsUnbilled")]
        public async Task<IActionResult> SetTransactionAsUnbilled(int transactionId)
        {
            return await SetBillingStatus(transactionId, BillingStatus.Unbilled);
        }

        [HttpPut("ModifyTransactionBillingStatus")]
        public async Task<IActionResult> SetBillingStatus(int transactionId, BillingStatus billingStatus)
        {
            var transaction = await _unitOfWork
                .TransactionRepository
                .GetById(transactionId)
                ;

            if (transaction == null)
            {
                return NotFound();
            }

            transaction.BillingStatus = billingStatus;

            await _unitOfWork.SaveChangesAsync();

            return CreatedAtAction(
                nameof(SetBillingStatus),
                new { id = transactionId },
                _mapper.Map<DTOs.Read.TransactionDtoRead>(transaction));
        }
    }
}