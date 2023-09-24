using Ride.Application.UseCase;
using Ride.Infra.Database;
using Ride.Infra.Repository;

namespace Ride.Tests.Integration;

public sealed class AccountServiceTests
{
    private readonly Signup _signup;
    private readonly GetAccount _getAccount;

    public AccountServiceTests()
    {
        var connection = new PgPromiseAdapter();
        var accountDao = new AccountDaoDatabase(connection);
        _signup = new Signup(accountDao);
        _getAccount = new GetAccount(accountDao);
    }

    [Fact(DisplayName = "Deve criar um passageiro")]
    public async void Signup_WhenDataAreValid_ShouldCreatePassenger()
    {
        InputSignUp input = new (
            "John Doe",
            $"john.doe{Guid.NewGuid().ToString()}@gmail.com",
            "95818705552",
            true,
            false,
            null
        );
        var output = await _signup.Execute(input);
        var account = await _getAccount.Execute(output);
        Assert.NotNull(account?.AccountId);
        Assert.Equal(input.Name, account.Name);
        Assert.Equal(input.Email, account.Email);
        Assert.Equal(input.Cpf, account.Cpf);
    }

    [Fact(DisplayName = "Não deve criar um passageiro com cpf inválido")]
    public async void Singup_WhenCpfInvalid_ShouldThrowException()
    {
        InputSignUp input = new (
            "John Doe",
            $"john.doe{Guid.NewGuid().ToString()}@gmail.com",
            "95818705500",
            true,
            false,
            null
        );
        var action = async () => await _signup.Execute(input);
        var exception = await Assert.ThrowsAsync<Exception>(action);
        Assert.Equal("Invalid cpf", exception.Message);
    }
    
    [Fact(DisplayName = "Não deve criar um passageiro com nome inválido")]
    public async void Singup_WhenNameInvalid_ShouldThrowException()
    {
        InputSignUp input = new (
            "John",
            $"john.doe{Guid.NewGuid().ToString()}@gmail.com",
            "95818705552",
            true,
            false,
            null
        );
        var action = async () => await _signup.Execute(input);
        var exception = await Assert.ThrowsAsync<Exception>(action);
        Assert.Equal("Invalid name", exception.Message);
    }
    
    [Fact(DisplayName = "Não deve criar um passageiro com email inválido")]
    public async void Singup_WhenEmailInvalid_ShouldThrowException()
    {
        InputSignUp input = new (
            "John Doe",
            $"john.doe@",
            "95818705552",
            true,
            false,
            null
        );
        var action = async () => await _signup.Execute(input);
        var exception = await Assert.ThrowsAsync<Exception>(action);
        Assert.Equal("Invalid email", exception.Message);
    }
    
    [Fact(DisplayName = "Não deve criar um passageiro com conta existente")]
    public async void Singup_WhenPassengerAlreadyExists_ShouldThrowException()
    {
        InputSignUp input = new (
            "John Doe",
            $"john.doe{Guid.NewGuid().ToString()}@gmail.com",
            "95818705552",
            true,
            false,
            null
        );
        await _signup.Execute(input);
        var action = async () => await _signup.Execute(input);
        var exception = await Assert.ThrowsAsync<Exception>(action);
        Assert.Equal("Account already exists", exception.Message);
    }
}