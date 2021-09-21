namespace API.Dto.Ledger
{
    public class GetTransactionDto
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public string TransactionType { get; set; }
        public string Description { get; set; }
        public bool status { get; set; }
        public string CreatedAt { get; set; }
    }
}