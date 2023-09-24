using Ride.Domain;
using Ride.Application.Repository;
using Ride.Infra.Database;

namespace Ride.Infra.Repository;

public class RideDaoDatabase : IRideDao
{
    private readonly IConnection _connection;

    public RideDaoDatabase(IConnection connection)
    {
        this._connection = connection;
    }

    public async Task Save(Domain.Ride ride)
    {
        var sql = @"INSERT INTO cccat13.ride 
                    (ride_id, passenger_id, driver_id, status, from_lat, from_long, to_lat, to_long, date) 
                    VALUES (@RideId, @PassengerId, @DriverId, @Status, @FromLat, @FromLong, @ToLat, @ToLong, @Date)";
        
        var parameters = new
        {
            ride.RideId,
            ride.PassengerId,
            ride.DriverId,
            ride.Status,
            ride.FromLat,
            ride.FromLong,
            ride.ToLat,
            ride.ToLong,
            ride.Date
        };

        await _connection.Query(sql, parameters);
    }

    public async Task Update(Domain.Ride ride)
    {
        var sql = "UPDATE cccat13.ride SET driver_id = @DriverId, status = @Status WHERE ride_id = @RideId";

        var parameters = new
        {
            ride.DriverId,
            ride.Status,
            ride.RideId
        };

        await _connection.Query(sql, parameters);
    }

    public async Task<Domain.Ride> GetById(string rideId)
    {
        var sql = "SELECT * FROM cccat13.ride WHERE ride_id = @RideId";
        var parameters = new { RideId = rideId };

        var rideData = await _connection.Query(sql, parameters);
        return MapRide(rideData.Single());
    }

    public async Task<IEnumerable<Domain.Ride>> GetActiveRidesByPassengerId(string passengerId)
    {
        var sql = "SELECT * FROM cccat13.ride WHERE passenger_id = @PassengerId AND status IN ('requested', 'accepted', 'in_progress')";
        var parameters = new { PassengerId = passengerId };

        var ridesData = await _connection.Query(sql, parameters);
        return MapRides(ridesData);
    }

    public async Task<IEnumerable<Domain.Ride>> GetActiveRidesByDriverId(string driverId)
    {
        var sql = "SELECT * FROM cccat13.ride WHERE driver_id = @DriverId AND status IN ('accepted', 'in_progress')";
        var parameters = new { DriverId = driverId };

        var ridesData = await _connection.Query(sql, parameters);
        return MapRides(ridesData);
    }

    private Domain.Ride MapRide(dynamic rideData)
    {
        if (rideData == null) return null!;

        return Domain.Ride.Restore(
            rideData.ride_id.ToString(),
            rideData.passenger_id.ToString(),
            rideData.driver_id.ToString(),
            rideData.status,
            (float)rideData.from_lat,
            (float)rideData.from_long,
            (float)rideData.to_lat,
            (float)rideData.to_long,
            rideData.date
        );
    }

    private IEnumerable<Domain.Ride> MapRides(IEnumerable<dynamic> ridesData)
    {
        var rides = new List<Domain.Ride>();
        foreach (var rideData in ridesData)
        {
            var ride = MapRide(rideData);
            rides.Add(ride);
        }

        return rides;
    }
}
