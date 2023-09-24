using Ride.Domain;
using Ride.Application.Repository;

namespace Ride.Application.UseCase;

public sealed class GetRide
{
    private readonly IRideDao _rideDao;

    public GetRide(IRideDao rideDao)
    {
        this._rideDao = rideDao;
    }

    public async Task<Domain.Ride> Execute(string rideId)
    {
        var ride = await _rideDao.GetById(rideId);
        return ride;
    }
}
