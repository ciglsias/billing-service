using Arkusnexus.Billing.Infrastructure;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public async Task<IActionResult> Get()
        {
            var result = (await _unitOfWork
                .TransactionRepository
                .GetAll()
                .ToListAsync())
                .Select(x => _mapper.Map<DTOs.Read.TransactionDtoRead>(x))
                ;

            return Ok(result);
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _unitOfWork
                .TransactionRepository
                .GetById(id);

            if (result == null)
            {
                return NotFound();
            }

            var dto = _mapper.Map<DTOs.Read.TransactionDtoRead>(result);

            return Ok(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] DTOs.Write.TransactionDtoWrite transaction)
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

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _unitOfWork
                .TransactionRepository
                .DeleteById(id);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}