using Ride.Domain;

namespace Ride.Tests.Unit;

public sealed class CpfValidatorTests
{
    [Theory(DisplayName = "Deve validar um cpf")]
    [InlineData("95818705552")]
    [InlineData("01234567890")]
    [InlineData("565.486.780-60")]
    [InlineData("147.864.110-00")]
    public void Validate_WhenCpfIsValid_ShouldReturnsTrue(string cpf)
    {
        CpfValidator validator = new();
        Assert.True(validator.Validate(cpf));
    }
    
    [Theory(DisplayName = "Não deve validar um cpf")]
    [InlineData("958.187.055-00")]
    [InlineData("958.187.055")]
    [InlineData("00000000000")]
    [InlineData("")]
    public void Validate_WhenCpfIsInValid_ShouldReturnsFalse(string cpf)
    {
        CpfValidator validator = new();
        Assert.False(validator.Validate(cpf));
    }
}