using DebtManager.Application.Services.Interfaces;
using DebtManager.Domain.DebtCalculations;
using System.Collections.Generic;
using System.Web.Http;

namespace DebtManager.Web.Controllers
{
    public class AggregatedMinimizedDebtsController : ApiController
    {
        private IAggregatedDebtService _aggregatedDebtService;

        public AggregatedMinimizedDebtsController(IAggregatedDebtService aggregatedDebtService)
        {
            _aggregatedDebtService = aggregatedDebtService;
        }

        [HttpGet]
        public IEnumerable<AggregatedDebt> Get()
        {
            return _aggregatedDebtService.GetAllMinimized();
        }
    }
}
