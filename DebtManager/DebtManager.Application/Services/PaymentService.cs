using DebtManager.Application.Services.Interfaces;
using DebtManager.Domain.DebtCalculations;
using DebtManager.Domain.Dtos;
using DebtManager.Domain.Entities;
using DebtManager.Infrastructure.EFCodeFirst;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DebtManager.Application.Services
{
    public class PaymentService : IPaymentService
    {
        IRepository _repository;

        public PaymentService(IRepository repository)
        {
            _repository = repository;
        }

        public IList<PaymentDto> GetAll()
        {
            return _repository.Payments.Include(p => p.Payer).Include(p => p.Receiver).ToList().Select(u => u.ToDto()).OrderByDescending(u => u.Date).ToList();
        }


        public PaymentDto Create(PaymentDto dto)
        {
            var entity = Payment.FromDto(dto, _repository.Users.First(u => u.Id == dto.Payer.Id), _repository.Users.First(u => u.Id == dto.Receiver.Id));

            _repository.Payments.Add(entity);

            _repository.SaveChanges();

            return entity.ToDto();
        }
    }
}
