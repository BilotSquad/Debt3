using DebtManager.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace DebtManager.Domain.DebtCalculations
{
    public class AggregatedDebt
    {
        public User Payer { get; set; }
        public User Receiver { get; set; }
        public int Amount { get; set; }

        public void Normalize()
        {
            if (Amount < 0)
            {
                User tmpUser = this.Payer;

                Payer = this.Receiver;
                Receiver = tmpUser;
                Amount = -this.Amount;
            }
        }

        public static bool ArrayContainsDebtFor(IEnumerable<AggregatedDebt> aggregatedDebts, User user1, User user2)
        {
            return aggregatedDebts.Any(ad => (ad.Payer.Id == user1.Id && ad.Receiver.Id == user2.Id) ||
                (ad.Payer.Id == user2.Id && ad.Receiver.Id == user1.Id)); 
        }
    }
}
