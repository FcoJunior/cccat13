using Ride.Application.Repository;

namespace Ride.Application.UseCase;

public sealed class StartRide
{
    private readonly IRideDao _rideDao;

    public StartRide(IRideDao rideDao)
    {
        this._rideDao = rideDao;
    }

    public async Task Execute(InputStartRide input)
    {
        var ride = await _rideDao.GetById(input.RideId);
        ride.Start();
        await _rideDao.Update(ride);
    }
}

public sealed record InputStartRide(string RideId);