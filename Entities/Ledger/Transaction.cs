using System.ComponentModel.DataAnnotations.Schema;
using Nas_Pos.Entities.Identity;

namespace API.Entities.Ledger
{
    public class Transaction : BaseClass
    {
        public Transaction()
        {
        }

        public Transaction(decimal amount, string transactionType, string description, bool status)
        {
            Amount = amount;
            TransactionType = transactionType;
            Description = description;
            this.status = status;
        }

        
        public decimal Amount { get; set; }
        public string TransactionType { get; set; }
        public string Description { get; set; }
        public bool status { get; set; }
    }
}