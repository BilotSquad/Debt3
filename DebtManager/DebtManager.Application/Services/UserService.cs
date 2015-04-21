using DebtManager.Application.Services.Interfaces;
using DebtManager.Domain.Dtos;
using DebtManager.Infrastructure.EFCodeFirst;
using System.Collections.Generic;
using System.Linq;

namespace DebtManager.Application.Services
{
    public class UserService : IUserService
    {
        IRepository _repository;

        public UserService(IRepository repository)
        {
            _repository = repository;
        }

        public IList<UserDto> GetAll()
        {
            return _repository.Users.ToList().Select(u => u.ToDto()).ToList();
        }
    }
}
