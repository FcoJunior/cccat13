using Ride.Application.Repository;
using Ride.Domain;

namespace Ride.Application.UseCase;

public sealed class GetAccount
{
    private readonly IAccountDao _accountDao;

    public GetAccount(IAccountDao accountDao)
    {
        this._accountDao = accountDao;
    }

    public async Task<Account> Execute(string accountId)
    {
        var account = await _accountDao.GetById(accountId);
        return account;
    }
}
