using Ride.Application.Repository;
using Ride.Domain;

namespace Ride.Infra.Repository;

using System.Collections.Generic;
using System.Threading.Tasks;

public class AccountDaoMemory : IAccountDao
{
    private List<Account> accounts = new();

    public async Task Save(Account account)
    {
        accounts.Add(account);
    }

    public async Task<Account> GetByEmail(string email)
    {
        var account = accounts.Find(a => a.Email == email);
        return MapAccount(account);
    }

    public async Task<Account> GetById(string accountId)
    {
        var account = accounts.Find(a => a.AccountId == accountId);
        return MapAccount(account);
    }

    private Account MapAccount(Account account)
    {
        if (account is null) return null!;
        return account;
    }
}
