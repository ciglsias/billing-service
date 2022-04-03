using AutoMapper;

namespace Arkusnexus.Billing.Web.Mapping
{
    public class BillingAutoMapperProfile : Profile
    {
        public BillingAutoMapperProfile()
        {
            CreateMap<Domain.Entities.Transaction, DTOs.Read.TransactionDtoRead>();
            CreateMap<DTOs.Write.TransactionDtoWrite, Domain.Entities.Transaction>();

            CreateMap<Domain.Entities.Invoice, DTOs.Read.InvoiceDtoRead>();
        }
    }
}