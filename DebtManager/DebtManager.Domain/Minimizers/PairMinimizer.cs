using DebtManager.Domain.DebtCalculations;
using System.Collections.Generic;
using DebtManager.Domain.Entities;
using System.Linq;


namespace DebtManager.Domain.Minimizers
{
    public class PairMinimizer
    {
        List<AggregatedDebt> _debts;

        public PairMinimizer(IEnumerable<AggregatedDebt> debts)
        {
            _debts = debts.ToList();
        }

        public IList<AggregatedDebt> Execute()
        {
            var users = _debts.Select(p => p.Payer).Union(_debts.Select(p => p.Receiver)).Distinct();

            AggregatedDebt ad1 = null;
            AggregatedDebt ad2 = null;

            InitAggregatedDebtPair(ref ad1, ref  ad2, users);

            while (ad1 != null && ad2 != null)
            {
                if (ad1.Amount > ad2.Amount)
                {
                    var line = _debts.FirstOrDefault(d => (ad1.Payer.Id == d.Payer.Id && ad2.Receiver.Id == d.Receiver.Id) || (ad1.Payer.Id == d.Receiver.Id && ad2.Receiver.Id == d.Payer.Id));

                    if (line == null)
                    {
                        line = new AggregatedDebt { Payer = ad1.Payer, Receiver = ad2.Receiver, Amount = ad2.Amount };
                        _debts.Add(line);
                    }
                    else
                    {
                        if (ad1.Payer == line.Payer)
                        {
                            line.Amount += ad2.Amount;
                        }
                        else
                        {
                            line.Amount -= ad2.Amount;
                            line.Normalize();
                        }
                    }

                    ad1.Amount -= ad2.Amount;
                    _debts.Remove(ad2);
                }
                // ad1.Amount < ad2.Amount
                else
                {
                    var line = _debts.FirstOrDefault(d => (ad1.Payer.Id == d.Payer.Id && ad2.Receiver.Id == d.Receiver.Id) || (ad1.Payer.Id == d.Receiver.Id && ad2.Receiver.Id == d.Payer.Id));

                    if (line == null)
                    {
                        line = new AggregatedDebt { Payer = ad1.Payer, Receiver = ad2.Receiver, Amount = ad1.Amount };
                        _debts.Add(line);
                    }
                    else
                    {
                        if (ad1.Payer == line.Payer)
                        {
                            line.Amount += ad1.Amount;
                        }
                        else
                        {
                            line.Amount -= ad1.Amount;
                            line.Normalize();
                        }
                    }

                    ad2.Amount -= ad1.Amount;
                    _debts.Remove(ad1);
                }

                ad1 = null;
                ad2 = null;

                InitAggregatedDebtPair(ref ad1, ref ad2, users);
            }

            _debts = _debts.OrderBy(ad => ad.Payer.Name).ThenBy(ad => ad.Receiver.Name).ToList();

            return _debts;
        }

        private void InitAggregatedDebtPair(ref AggregatedDebt ad1, ref AggregatedDebt ad2, IEnumerable<User> users)
        {
            foreach (var u in users)
            {
                ad1 = _debts.FirstOrDefault(d => u.Id == d.Receiver.Id);

                if (ad1 == null) continue;

                ad2 = _debts.FirstOrDefault(d => u.Id == d.Payer.Id);

                if (ad2 == null) continue;

                break;
            }
        }
    }
}
