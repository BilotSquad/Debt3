using DebtManager.Domain.Entities;
using System.Data.Entity;

namespace DebtManager.Infrastructure.EFCodeFirst
{
    public interface IRepository
    {
        int SaveChanges();

        IDbSet<Debt> Debts { get; set; }
        IDbSet<Payment> Payments { get; set; }
        IDbSet<User> Users { get; set; }
    }
}
