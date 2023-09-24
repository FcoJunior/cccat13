using Ride.Application.Repository;
using Ride.Domain;
using Ride.Infra.Database;

namespace Ride.Infra.Repository;

public class AccountDaoDatabase : IAccountDao
{
    private readonly IConnection _connection;

    public AccountDaoDatabase(IConnection connection)
    {
        this._connection = connection;
    }

    public async Task Save(Account account)
    {
        var sql = @"INSERT INTO cccat13.account 
                    (account_id, name, email, cpf, car_plate, is_passenger, is_driver, date, is_verified, verification_code) 
                    VALUES (@AccountId, @Name, @Email, @Cpf, @CarPlate, @IsPassenger, @IsDriver, @Date, @IsVerified, @VerificationCode)";
        
        var parameters = new
        {
            AccountId = Guid.Parse(account.AccountId),
            account.Name,
            account.Email,
            account.Cpf,
            account.CarPlate,
            account.IsPassenger,
            account.IsDriver,
            account.Date,
            IsVerified = false,
            VerificationCode = Guid.Parse(account.VerificationCode)
        };

        await _connection.Query(sql, parameters);
    }

    public async Task<Account> GetByEmail(string email)
    {
        var sql = "SELECT * FROM cccat13.account WHERE email = @Email";
        var parameters = new { Email = email };

        var accountData = await _connection.Query(sql, parameters);
        return MapAccount(accountData.SingleOrDefault()!);
    }

    public async Task<Account> GetById(string accountId)
    {
        var sql = "SELECT * FROM cccat13.account WHERE account_id = @AccountId";
        var parameters = new { AccountId = Guid.Parse(accountId) };

        var accountData = await _connection.Query(sql, parameters);
        return MapAccount(accountData.SingleOrDefault()!);
    }

    private Account MapAccount(dynamic accountData)
    {
        if (accountData == null) return null!;

        return Account.Restore(
            accountData.account_id.ToString(),
            accountData.name,
            accountData.email,
            accountData.cpf,
            accountData.is_passenger,
            accountData.is_driver,
            accountData.car_plate,
            accountData.date,
            accountData.verification_code.ToString()
        );
    }
}
