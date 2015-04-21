using DebtManager.Domain.Dtos;
using System.Collections.Generic;

namespace DebtManager.Application.Services.Interfaces
{
    public interface IUserService
    {
        IList<UserDto> GetAll(); 
    }
}
