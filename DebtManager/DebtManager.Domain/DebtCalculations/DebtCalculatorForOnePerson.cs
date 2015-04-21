using DebtManager.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace DebtManager.Domain.DebtCalculations
{
    public class DebtCalculatorForOnePerson
    {
        User _user;
        IEnumerable<Payment> _payments;
        IEnumerable<Debt> _debts;

        public DebtCalculatorForOnePerson(User user, IEnumerable<Payment> payments, IEnumerable<Debt> debts)
        {
            _user = user;
            _payments = payments;
            _debts = debts;
        }

        public IList<AggregatedDebt> Execute()
        {
            var users = _payments.Select(p => p.Payer).Union(_payments.Select(p => p.Receiver)).Union(_debts.Select(p => p.Payer)).Union(_debts.Select(p => p.Receiver)).Except(new[] { _user }).Distinct();

            var aggregatedDebts = new List<AggregatedDebt>();

            foreach(User u in users)
            {
                aggregatedDebts.Add(new DebtCalculatorForTwoPeople(_user, u, _payments, _debts).Execute());
            }

            aggregatedDebts.RemoveAll(ad => ad.Amount == 0);
            aggregatedDebts = aggregatedDebts.OrderBy(ad => ad.Payer.Name).ThenBy(ad => ad.Receiver.Name).ToList();

            return aggregatedDebts;
        }
    }
}
