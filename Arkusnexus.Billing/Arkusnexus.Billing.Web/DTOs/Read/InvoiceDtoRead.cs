namespace Arkusnexus.Billing.Web.DTOs.Read
{
    public class InvoiceDtoRead
    {
        public int Id { get; set; }

        public DateTime DateTime { get; set; }

        public bool Paid { get; set; }

        public List<TransactionDtoRead> Transactions { get; set; } = null!;
    }
}
