﻿using DebtManager.Domain.Dtos;
using System;

namespace DebtManager.Domain.Entities
{
    public class Debt
    {
        public int Id { get; private set; }
        public User Payer { get; private set; }
        public User Receiver { get; private set; }
        public int Amount { get; private set; }
        public DateTime Date { get; private set; }
        public string Reason { get; private set; }

        private Debt() { }

        public DebtDto ToDto()
        {
            var dto = new DebtDto
            {
                Id = this.Id,
                Payer = this.Payer != null ? this.Payer.ToDto() : null,
                Receiver = this.Receiver != null ? this.Receiver.ToDto() : null,
                Amount = this.Amount,
                Date = this.Date,
                Reason = this.Reason,
                ForamtedDate = this.Date.ToString("f")
            };

            return dto;
        }

        public static Debt FromDto(DebtDto dto, User payer, User receiver)
        {
            if (payer.Id == receiver.Id) throw new Exception("Payer and Receiver must not be the same person.");
            if (dto.Amount <= 0) throw new Exception("Amount should be greater than 0.");

            Debt entity = new Debt();

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
