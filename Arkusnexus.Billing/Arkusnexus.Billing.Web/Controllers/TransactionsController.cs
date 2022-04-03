using Arkusnexus.Billing.Domain.Entities;
using Arkusnexus.Billing.Infrastructure;
using Arkusnexus.Billing.Web.DTOs.Read;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Arkusnexus.Billing.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransactionsController : ControllerBase
    {
        readonly IBillingUnitOfWork _unitOfWork;
        readonly IMapper _mapper;

        public TransactionsController(IBillingUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;

            _mapper = mapper;
        }

        [HttpGet]
        public IEnumerable<DTOs.Read.TransactionDtoRead> Get()
        {
            return _unitOfWork
                .TransactionRepository
                .GetAll()
                .Select(x => _mapper.Map<DTOs.Read.TransactionDtoRead>(x));
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] DTOs.Write.TransactionDtoWrite transaction)
        {
            var added = _unitOfWork
                .TransactionRepository
                .Add(_mapper.Map<Domain.Entities.Transaction>(transaction))
                ;

            await _unitOfWork.SaveChangesAsync();

            return CreatedAtAction(
                nameof(Get),
                new { id = added.Id }, 
                _mapper.Map<DTOs.Read.TransactionDtoRead>(added));
        }
    }
}