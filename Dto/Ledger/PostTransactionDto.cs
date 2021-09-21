namespace API.Dto.Ledger
{
    public class PostTransactionDto
    {
        public decimal Amount { get; set; }
        public string TransactionType { get; set; }
        public string Description { get; set; }
        
    }
}