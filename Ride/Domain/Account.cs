using System.Text.RegularExpressions;

namespace Ride.Domain;

public sealed record Account
{
    public string AccountId { get; init; }
    public string Name { get; init; }
    public string Email { get; init; }
    public string Cpf { get; init; }
    public bool IsPassenger { get; init; }
    public bool IsDriver { get; init; }
    public string CarPlate { get; init; }
    public DateTime Date { get; init; }
    public string VerificationCode { get; init; }

    private Account() { }
    
    public static Account Create(string name, string email, string cpf, bool isPassenger, bool isDriver, string carPlate)
    {
        if (!Regex.IsMatch(name, @"^[a-zA-Z]+ [a-zA-Z]+$")) throw new Exception("Invalid name");
        if (!Regex.IsMatch(email, @"^(.+)@(.+)$")) throw new Exception("Invalid email");
        var cpfValidator = new CpfValidator();
        if (!cpfValidator.Validate(cpf)) throw new Exception("Invalid cpf");
        if (isDriver && !Regex.IsMatch(carPlate, @"^[A-Z]{3}[0-9]{4}$")) throw new Exception("Invalid plate");
        var accountId = Guid.NewGuid().ToString();
        var verificationCode = Guid.NewGuid().ToString();
        var date = DateTime.Now;
        return new Account
        {
            AccountId = accountId,
            Name = name,
            Email = email,
            Cpf = cpf,
            IsPassenger = isPassenger,
            IsDriver = isDriver,
            CarPlate = carPlate,
            Date = date,
            VerificationCode = verificationCode
        };
    }

    public static Account Restore(string accountId, string name, string email, string cpf, bool isPassenger,
        bool isDriver, string carPlate, DateTime date, string verificationCode)
        => new()
        {
            AccountId = accountId,
            Name = name,
            Email = email,
            Cpf = cpf,
            IsPassenger = isPassenger,
            IsDriver = isDriver,
            CarPlate = carPlate,
            Date = date,
            VerificationCode = verificationCode
        };
}