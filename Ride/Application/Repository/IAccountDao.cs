using Ride.Domain;

namespace Ride.Application.Repository;

public interface IAccountDao
{
    Task Save(Account account);
    Task<Account?> GetByEmail(string email);
    Task<Account?> GetById(string accountId);
}