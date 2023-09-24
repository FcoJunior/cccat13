using Ride.Application.Repository;

namespace Ride.Application.UseCase;

public sealed class AcceptRide
{
    private readonly IRideDao _rideDao;
    private readonly IAccountDao _accountDao;

    public AcceptRide(IRideDao rideDao, IAccountDao accountDao)
    {
        this._rideDao = rideDao;
        this._accountDao = accountDao;
    }

    public async Task Execute(InputAcceptRide input)
    {
        var account = await _accountDao.GetById(input.DriverId);
        if (account is not { IsDriver: true }) throw new Exception("Account is not from a driver");
        var ride = await _rideDao.GetById(input.RideId);
        ride.Accept(input.DriverId);
        var activeRides = await _rideDao.GetActiveRidesByDriverId(input.DriverId);
        if (activeRides.Any()) throw new Exception("Driver is already in another ride");
        await _rideDao.Update(ride);
    }
}

public sealed record InputAcceptRide(string RideId, string DriverId);