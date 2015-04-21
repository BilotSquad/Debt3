using DebtManager.Application.Services.Interfaces;
using DebtManager.Domain.DebtCalculations;
using DebtManager.Domain.Minimizers;
using DebtManager.Infrastructure.EFCodeFirst;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;


namespace DebtManager.Application.Services
{
    public class AggregatedDebtService : IAggregatedDebtService
    {
        IRepository _repository;

        public AggregatedDebtService(IRepository repository)
        {
            _repository = repository;
        }

        public IList<AggregatedDebt> GetAll()
        {

            //var tmp = new DebtCalculatorForTwoPeople(_repository.Users.First(u => u.Id == 1),
            //    _repository.Users.First(u => u.Id == 2),
            //    _repository.Payments.Include(p => p.Payer).Include(p => p.Receiver).ToArray(),
            //    _repository.Debts.Include(p => p.Payer).Include(p => p.Receiver).ToArray())
            //    .Execute();

            //var tmp2 = new DebtCalculatorForOnePerson(_repository.Users.First(u => u.Id == 1),
            //    _repository.Payments.Include(p => p.Payer).Include(p => p.Receiver).ToArray(),
            //    _repository.Debts.Include(p => p.Payer).Include(p => p.Receiver).ToArray())
            //    .Execute();

            return new DebtCalculatorForMultiplePeople(
                _repository.Payments.Include(p => p.Payer).Include(p => p.Receiver).ToArray(),
                _repository.Debts.Include(p => p.Payer).Include(p => p.Receiver).ToArray())
                .Execute();
        }


        public IList<AggregatedDebt> GetAllMinimized()
        {
           return new PairMinimizer(this.GetAll()).Execute();
        }
    }
}
