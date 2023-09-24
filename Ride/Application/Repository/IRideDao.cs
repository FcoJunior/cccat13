using Ride.Domain;

namespace Ride.Application.Repository;

public interface IRideDao
{
    Task Save(Domain.Ride ride);
    Task Update(Domain.Ride ride);
    Task<Domain.Ride> GetById(string rideId);
    Task<IEnumerable<Domain.Ride>> GetActiveRidesByPassengerId(string passengerId);
    Task<IEnumerable<Domain.Ride>> GetActiveRidesByDriverId(string driverId);
}
