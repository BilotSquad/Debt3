using DebtManager.Domain.Dtos;

namespace DebtManager.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }


        private User() { }

        public UserDto ToDto()
        {
            UserDto userDto = new UserDto();

            userDto.Id = this.Id;
            userDto.Name = this.Name;

            return userDto;
        }
    }
}
