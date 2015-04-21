using DebtManager.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace DebtManager.Domain.DebtCalculations
{
    public class DebtCalculatorForTwoPeople
    {
        User _user1;
        User _user2;
        IEnumerable<Payment> _payments;
        IEnumerable<Debt> _debts;

        public DebtCalculatorForTwoPeople(User user1, User user2, IEnumerable<Payment> payments, IEnumerable<Debt> debts)
        {
            _user1 = user1;
            _user2 = user2;
            _payments = payments;
            _debts = debts;
        }

        public AggregatedDebt Execute()
        {
            var aggregatedDebt = new AggregatedDebt();

            aggregatedDebt.Payer = _user1;
            aggregatedDebt.Receiver = _user2;

            aggregatedDebt.Amount = 0;

            if (_user1.Id == _user2.Id) return aggregatedDebt;

            aggregatedDebt.Amount -= _payments.Where(p => p.Payer.Id == _user1.Id && p.Receiver.Id == _user2.Id).Sum(p => p.Amount);
            aggregatedDebt.Amount += _payments.Where(p => p.Payer.Id == _user2.Id && p.Receiver.Id == _user1.Id).Sum(p => p.Amount);
            aggregatedDebt.Amount += _debts.Where(p => p.Payer.Id == _user1.Id && p.Receiver.Id == _user2.Id).Sum(p => p.Amount);
            aggregatedDebt.Amount -= _debts.Where(p => p.Payer.Id == _user2.Id && p.Receiver.Id == _user1.Id).Sum(p => p.Amount);

            aggregatedDebt.Normalize();

            return aggregatedDebt;
        }
    }
}
