using Ride.Application.Repository;
using Ride.Domain;
using Ride.Infra.Gateway;

namespace Ride.Application.UseCase;

public sealed class Signup
{
    private readonly IAccountDao _accountDao;
    private readonly CpfValidator _cpfValidator;
    private readonly MailerGateway _mailerGateway;

    public Signup(IAccountDao accountDao)
    {
        this._accountDao = accountDao;
        this._cpfValidator = new CpfValidator();
        this._mailerGateway = new MailerGateway();
    }

    public async Task<string> Execute(InputSignUp input)
    {
        var existingAccount = await _accountDao.GetByEmail(input.Email);
        if (existingAccount != null) throw new Exception("Account already exists");
        var account = Account.Create(input.Name, input.Email, input.Cpf, input.IsPassenger, input.IsDriver, input.CarPlate);
        await _accountDao.Save(account);
        await _mailerGateway.Send(account.Email, "Verification", $"Please verify your code at first login {account.VerificationCode}");
        return account.AccountId;
    }
}

public sealed record InputSignUp(string Name, string Email, string Cpf, bool IsPassenger, bool IsDriver, string CarPlate);