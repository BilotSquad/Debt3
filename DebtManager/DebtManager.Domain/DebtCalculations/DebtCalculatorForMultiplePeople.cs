using DebtManager.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace DebtManager.Domain.DebtCalculations
{
    public class DebtCalculatorForMultiplePeople
    {
        IEnumerable<Payment> _payments;
        IEnumerable<Debt> _debts;

        public DebtCalculatorForMultiplePeople(IEnumerable<Payment> payments, IEnumerable<Debt> debts)
        {
            _payments = payments;
            _debts = debts;
        }

        public List<AggregatedDebt> Execute()
        {
            var users = _payments.Select(p => p.Payer).Union(_payments.Select(p => p.Receiver)).Union(_debts.Select(p => p.Payer)).Union(_debts.Select(p => p.Receiver)).Distinct();

            var aggregatedDebts = new List<AggregatedDebt>();

            foreach (User u1 in users)
            {
                foreach (User u2 in users)
                {
                    if (AggregatedDebt.ArrayContainsDebtFor(aggregatedDebts, u1, u2) || u1.Id == u2.Id) continue;

                    aggregatedDebts.Add(new DebtCalculatorForTwoPeople(u1, u2, _payments, _debts).Execute());
                }
            }

            aggregatedDebts.RemoveAll(ad => ad.Amount == 0);
            aggregatedDebts = aggregatedDebts.OrderBy(ad => ad.Payer.Name).ThenBy(ad => ad.Receiver.Name).ToList();

            return aggregatedDebts;
        }
    }
}
