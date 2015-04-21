using DebtManager.Domain.Dtos;
using System.Collections.Generic;

namespace DebtManager.Application.Services.Interfaces
{
    public interface IDebtService
    {
        IList<DebtDto> GetAll();
        DebtDto Create(DebtDto dto);
    }
}
