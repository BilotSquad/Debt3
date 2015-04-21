using DebtManager.Domain.Dtos;
using System;

namespace DebtManager.Domain.Entities
{
    public class Payment
    {
        public int Id { get; private set; }
        public User Payer { get; private set; }
        public User Receiver { get; private set; }
        public int Amount { get; private set; }
        public DateTime Date { get; private set; }
        public string Reason { get; private set; }

        private Payment() { }


        public PaymentDto ToDto()
        {
            PaymentDto dto = new PaymentDto();

            dto.Id = this.Id;
            dto.Payer = this.Payer != null ? this.Payer.ToDto() : null;
            dto.Receiver = this.Receiver != null ? this.Receiver.ToDto() : null;
            dto.Amount = this.Amount;
            dto.Date = this.Date;
            dto.Reason = this.Reason;

            return dto;
        }

        public static Payment FromDto(PaymentDto dto, User payer, User receiver)
        {
            if (payer.Id == receiver.Id) throw new Exception("Payer and Receiver must not be the same person.");
            if (dto.Amount <= 0) throw new Exception("Amount should be greater than 0.");

            Payment entity = new Payment();

            entity.Id = dto.Id;
            entity.Payer = payer;
            entity.Receiver = receiver;
            entity.Amount = dto.Amount;
            entity.Date = DateTime.Now;
            entity.Reason = dto.Reason;

            return entity;
        }
    }
}