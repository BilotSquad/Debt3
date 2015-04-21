using System;

namespace DebtManager.Domain.Dtos
{
    public class DebtDto
    {
        public int Id { get; set; }
        public UserDto Payer { get; set; }
        public UserDto Receiver { get; set; }
        public int Amount { get; set; }
        public DateTime Date { get; set; }
        public string Reason { get; set; }
    }
}
