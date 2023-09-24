namespace Ride.Tests.Unit;
using Ride.Domain;

public class RideTests
{
    [Fact(DisplayName = "Deve criar uma ride")]
    public void Create_WhenDataIsValid_ShouldCreated()
    {
        var ride = Ride.Create("", 0, 0, 0, 0);
        Assert.NotNull(ride);
        Assert.Equal("requested", ride.GetStatus());
    }
    
    [Fact(DisplayName = "Deve aceitar uma ride")]
    public void Accept_WhenRideIsValid_ShouldAccept()
    {
        var ride = Ride.Create("", 0, 0, 0, 0);
        ride.Accept("");
        Assert.Equal("accepted", ride.GetStatus());
    }
    
    [Fact(DisplayName = "Não deve aceitar uma ride quando o status for inválido")]
    public void Accept_WhenStatusIsInvalid_ShouldThrowException()
    {
        var ride = Ride.Restore("", "", "", "",0, 0, 0, 0, DateTime.Now);
        var action = () => ride.Accept("");
        var exception = Assert.Throws<Exception>(action);
        Assert.Equal("The ride is not requested", exception.Message);
    }
    
    [Fact(DisplayName = "Deve iniciar uma ride")]
    public void Start_WhenRideIsValid_ShouldStart()
    {
        var ride = Ride.Create("", 0, 0, 0, 0);
        ride.Accept("");
        ride.Start();
        Assert.Equal("in_progress", ride.GetStatus());
    }
    
    [Fact(DisplayName = "Não deve iniciar uma ride quando o status for inválido")]
    public void Start_WhenStatusIsInvalid_ShouldThrowException()
    {
        var ride = Ride.Restore("", "", "", "",0, 0, 0, 0, DateTime.Now);
        var action = () => ride.Start();
        var exception = Assert.Throws<Exception>(action);
        Assert.Equal("The ride is not accepted", exception.Message);
    }
}