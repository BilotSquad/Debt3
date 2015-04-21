using DebtManager.Application.Services.Interfaces;
using DebtManager.Domain.Dtos;
using System.Collections.Generic;
using System.Web.Http;

namespace DebtManager.Web.Controllers
{
    public class UsersController : ApiController
    {
        private IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        // GET api/values
        public IEnumerable<UserDto> Get()
        {
            return _userService.GetAll();
        }
    }
}
