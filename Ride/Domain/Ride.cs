namespace Ride.Domain;

public sealed class Ride
{
    public string DriverId { get; private set; }
    public string Status { get; private set; }
    public string RideId { get; private set; }
    public string PassengerId { get; private set; }
    public double FromLat { get; private set; }
    public double FromLong { get; private set; }
    public double ToLat { get; private set; }
    public double ToLong { get; private set; }
    public DateTime Date { get; private set; }
    
    private Ride() { }

    public static Ride Create(string passengerId, double fromLat, double fromLong, double toLat, double toLong)
        => new() 
        {
            RideId = Guid.NewGuid().ToString(),
            Status = "requested",
            Date = DateTime.Now,
            PassengerId = passengerId,
            FromLat = fromLat,
            FromLong = fromLong,
            ToLat = toLat,
            ToLong = toLong
        };

    public static Ride Restore(string rideId, string passengerId, string driverId, string status, double fromLat, double fromLong, double toLat, double toLong, DateTime date)
        =>  new()
        {
            RideId = rideId,
            Status = status,
            Date = date,
            PassengerId = passengerId,
            FromLat = fromLat,
            FromLong = fromLong,
            ToLat = toLat,
            ToLong = toLong,
            DriverId = driverId
        };

    public void Accept(string driverId)
    {
        if (this.Status != "requested") throw new Exception("The ride is not requested");
        this.DriverId = driverId;
        this.Status = "accepted";
    }

    public void Start()
    {
        if (this.Status != "accepted") throw new Exception("The ride is not accepted");
        this.Status = "in_progress";
    }

    public string GetStatus() => this.Status;
}