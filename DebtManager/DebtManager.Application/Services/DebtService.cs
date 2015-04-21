using DebtManager.Application.Services.Interfaces;
using DebtManager.Domain.Dtos;
using DebtManager.Domain.Entities;
using DebtManager.Infrastructure.EFCodeFirst;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DebtManager.Application.Services
{
    public class DebtService : IDebtService
    {
        IRepository _repository;

        public DebtService(IRepository repository)
        {
            _repository = repository;
        }

        public IList<DebtDto> GetAll()
        {
            return _repository.Debts.Include(p => p.Payer).Include(p => p.Receiver).ToList().Select(u => u.ToDto()).OrderByDescending(u => u.Date).ToList();
        }


        public DebtDto Create(DebtDto dto)
        {
            var entity = Debt.FromDto(dto, _repository.Users.First(u => u.Id == dto.Payer.Id), _repository.Users.First(u => u.Id == dto.Receiver.Id));

            _repository.Debts.Add(entity);

            _repository.SaveChanges();

            return entity.ToDto();
        }
    }
}
