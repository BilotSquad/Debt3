using DebtManager.Domain.Dtos;
using System.Collections.Generic;

namespace DebtManager.Application.Services.Interfaces
{
    public interface IPaymentService
    {
        IList<PaymentDto> GetAll();
        PaymentDto Create(PaymentDto dto);
    }
}
