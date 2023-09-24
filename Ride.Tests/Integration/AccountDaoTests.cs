using Ride.Application.Repository;
using Ride.Domain;
using Ride.Infra.Database;
using Ride.Infra.Repository;

namespace Ride.Tests.Integration;

public sealed class AccountDaoTests : IDisposable
{
    private readonly IConnection _connection;
    private readonly IAccountDao _accountDao;

    public AccountDaoTests()
    {
        _connection = new PgPromiseAdapter();
        _accountDao = new AccountDaoDatabase(_connection);
    }

    [Fact(DisplayName = "Deve criar um registro na tabela account e consultar por email")]
    public async void Create_WhenDataAreValid_ShouldCreateAccountInDatabase()
    {
        var account = Account.Create("John Doe", $"john.doe{Guid.NewGuid().ToString()}@gmail.com", "95818705552", true, false, "");
        await _accountDao.Save(account);
        var savedAccount = await _accountDao.GetByEmail(account.Email);
        Assert.NotNull(savedAccount?.AccountId);
        Assert.NotNull(savedAccount?.Date);
        Assert.Equal(account.Name, savedAccount.Name);
        Assert.Equal(account.Email, savedAccount.Email);
        Assert.Equal(account.Cpf, savedAccount.Cpf);
        Assert.Equal(account.VerificationCode, savedAccount.VerificationCode);
        Assert.True(savedAccount.IsPassenger);
    }

    public void Dispose()
    {
        this._connection.Close();
    }
}