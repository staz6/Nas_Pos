using System.Collections.Generic;
using API.Entities.OrderAggregate;
using Nas_Pos.Entities.Identity;

namespace API.Entities.Ledger
{
    public class Ledger : BaseClass
    {
        public Ledger()
        {
        }

        public Ledger(Order order, decimal totalAmount, decimal amountPaid, decimal amountRemaining, bool isDebit, List<Transaction> transactions)
        {
            Order = order;
            TotalAmount = totalAmount;
            AmountPaid = amountPaid;
            AmountRemaining = amountRemaining;
            IsDebit = isDebit;
            Transactions = transactions;
        }

        public Order Order { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal AmountPaid { get; set; }
        public decimal AmountRemaining { get; set; }
        public bool IsDebit { get; set; }
        public List<Transaction> Transactions { get; set; }
    }
}