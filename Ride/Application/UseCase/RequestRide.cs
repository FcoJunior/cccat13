using Ride.Domain;
using Ride.Application.Repository;

namespace Ride.Application.UseCase;

public sealed class RequestRide
{
    private readonly IRideDao _rideDao;
    private readonly IAccountDao _accountDao;

    public RequestRide(IRideDao rideDao, IAccountDao accountDao)
    {
        this._rideDao = rideDao;
        this._accountDao = accountDao;
    }

    public async Task<Dictionary<string, string>> Execute(InputRequestRide input)
    {
        var account = await _accountDao.GetById(input.PassengerId);
        if (account == null || !account.IsPassenger)
        {
            throw new Exception("Account is not from a passenger");
        }

        var activeRides = await _rideDao.GetActiveRidesByPassengerId(input.PassengerId);
        if (activeRides.Any())
        {
            throw new Exception("This passenger already has an active ride");
        }

        var ride = Domain.Ride.Create(input.PassengerId, input.From.Lat, input.From.Long, input.To.Lat, input.To.Long);
        await _rideDao.Save(ride);

        var result = new Dictionary<string, string>
        {
            { "rideId", ride.RideId }
        };

        return result;
    }
}

public sealed record InputRequestRide(string PassengerId, Location From, Location To);

public sealed record Location(double Lat, double Long);
