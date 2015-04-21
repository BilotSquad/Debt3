using DebtManager.Domain.DebtCalculations;
using System.Collections.Generic;

namespace DebtManager.Application.Services.Interfaces
{
    public interface IAggregatedDebtService
    {
        IList<AggregatedDebt> GetAll();
        IList<AggregatedDebt> GetAllMinimized();
    }
}
