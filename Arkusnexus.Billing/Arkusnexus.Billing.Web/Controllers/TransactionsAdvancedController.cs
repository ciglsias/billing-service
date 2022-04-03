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
        private readonly IBillingUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TransactionsAdvancedController(IBillingUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;

            _mapper = mapper;
        }

        [HttpPost("GenerateInvoice")]
        public async Task<IActionResult> GenerateInvoice(DateTime from, DateTime to)
        {
            List<Transaction>? transactions = await _unitOfWork
                .TransactionRepository
                .GetAll()
                .Where(t => t.DateTime >= from && t.DateTime <= to && t.BillingStatus == BillingStatus.Unbilled)
                .ToListAsync()
                ;

            if (transactions.Count == 0)
            {
                return NotFound("Not Found Transactions");
            }

            Invoice? newInvoice = new Invoice()
            {
                Transactions = transactions,
                DateTime = DateTime.Now,
            };

            foreach (Transaction? transaction in transactions)
            {
                transaction.BillingStatus = BillingStatus.Billed;
            }

            _unitOfWork.InvoiceRepository.Add(newInvoice);

            await _unitOfWork.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GenerateInvoice),
                _mapper.Map<DTOs.Read.InvoiceDtoRead>(newInvoice));
        }

        [HttpPut("SetInvoiceAsPaid")]
        public async Task<IActionResult> SetInvoiceAsPaid(int invoiceId)
        {
            Invoice? invoice = await _unitOfWork.InvoiceRepository.GetById(invoiceId);

            if (invoice == null)
            {
                return NotFound();
            }

            invoice.Paid = true;

            foreach (Transaction? transaction in invoice.Transactions)
            {
                transaction.BillingStatus = BillingStatus.Paid;
            }

            await _unitOfWork.SaveChangesAsync();

            return CreatedAtAction(
                nameof(SetInvoiceAsPaid),
                new { id = invoiceId },
                _mapper.Map<DTOs.Read.InvoiceDtoRead>(invoice));
        }

        [HttpPut("SetIndividualTransactionAsPaid")]
        public async Task<IActionResult> SetIndividualTransactionAsPaid(int transactionId)
        {
            return await ModifyIndividualTransactionBillingStatus(transactionId, BillingStatus.Paid);
        }

        [HttpPut("SetIndividualTransactionAsBilled")]
        public async Task<IActionResult> SetIndividualTransactionAsBilled(int transactionId)
        {
            return await ModifyIndividualTransactionBillingStatus(transactionId, BillingStatus.Billed);
        }

        [HttpPut("SetIndividualTransactionAsUnbilled")]
        public async Task<IActionResult> SetIndividualTransactionAsUnbilled(int transactionId)
        {
            return await ModifyIndividualTransactionBillingStatus(transactionId, BillingStatus.Unbilled);
        }

        [HttpPut("ModifyIndividualTransactionBillingStatus")]
        public async Task<IActionResult> ModifyIndividualTransactionBillingStatus(int transactionId, BillingStatus billingStatus)
        {
            Transaction? transaction = await _unitOfWork
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
                nameof(ModifyIndividualTransactionBillingStatus),
                new { id = transactionId },
                _mapper.Map<DTOs.Read.TransactionDtoRead>(transaction));
        }
    }
}