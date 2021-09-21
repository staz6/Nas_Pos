using API.Entities.Ledger;
using Nas_Pos.Specification;

namespace API.Specification
{
    public class LedgerWithOrderAndTransaction : BaseSpecification<Ledger>
    {
        public LedgerWithOrderAndTransaction()
        {
            AddInclude( x => x.Order);
            AddInclude( x => x.Transactions);
        }
        public LedgerWithOrderAndTransaction(int id) : base(x => x.Id==id)
        {
            AddInclude( x => x.Order);
            AddInclude( x => x.Transactions);
        }
    }
}