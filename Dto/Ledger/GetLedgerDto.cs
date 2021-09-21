using System.Collections.Generic;

namespace API.Dto.Ledger
{
    public class GetLedgerDto
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal AmountPaid { get; set; }
        public decimal AmountRemaining { get; set; }
        public bool IsDebit { get; set; }
        public string CreatedAt { get; set; }
        public IReadOnlyList<GetTransactionDto> Transactions { get; set; }
        
    }
}