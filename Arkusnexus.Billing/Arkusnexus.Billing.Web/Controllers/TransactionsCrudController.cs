using Arkusnexus.Billing.Infrastructure;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Arkusnexus.Billing.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransactionsCrudController : ControllerBase
    {
        private readonly IBillingUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TransactionsCrudController(IBillingUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;

            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            IEnumerable<DTOs.Read.TransactionDtoRead>? result = (await _unitOfWork
                .TransactionRepository
                .GetAll()
                .ToListAsync())
                .Select(x => _mapper.Map<DTOs.Read.TransactionDtoRead>(x))
                ;

            return Ok(result);
        }

        [HttpGet("GetPage")]
        public async Task<IActionResult> GetPage(int start, int length)
        {
            IEnumerable<DTOs.Read.TransactionDtoRead>? result = (await _unitOfWork
                .TransactionRepository
                .GetAll()
                .Skip(start - 1)
                .Take(length)
                .ToListAsync())
                .Select(x => _mapper.Map<DTOs.Read.TransactionDtoRead>(x))
                ;

            return Ok(result);
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(int id)
        {
            Domain.Entities.Transaction? result = await _unitOfWork
                .TransactionRepository
                .GetById(id);

            if (result == null)
            {
                return NotFound();
            }

            DTOs.Read.TransactionDtoRead? dto = _mapper.Map<DTOs.Read.TransactionDtoRead>(result);

            return Ok(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] DTOs.Write.TransactionDtoWrite transaction)
        {
            Domain.Entities.Transaction? transactionEntity = _mapper.Map<Domain.Entities.Transaction>(transaction);

            transactionEntity.DateTime = DateTime.Now;

            Domain.Entities.Transaction? added = _unitOfWork
                .TransactionRepository
                .Add(transactionEntity)
                ;

            await _unitOfWork.SaveChangesAsync();

            return CreatedAtAction(
                nameof(Create),
                new { id = added.Id },
                _mapper.Map<DTOs.Read.TransactionDtoRead>(added));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            bool result = await _unitOfWork
                .TransactionRepository
                .DeleteById(id);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> Update(int id, DTOs.Write.TransactionDtoWrite transaction)
        {
            Domain.Entities.Transaction? found = await _unitOfWork
            .TransactionRepository
            .GetById(id);

            if (found == null)
            {
                return NotFound();
            }

            Domain.Entities.Transaction? entity = _mapper.Map<Domain.Entities.Transaction>(transaction);

            entity.Id = id;

            Domain.Entities.Transaction? entityUpdated = await _unitOfWork
                .TransactionRepository
                .Update(entity);

            await _unitOfWork.SaveChangesAsync();

            return CreatedAtAction(
                nameof(Update),
                new { id = id },
                _mapper.Map<DTOs.Read.TransactionDtoRead>(entityUpdated));
        }
    }
}