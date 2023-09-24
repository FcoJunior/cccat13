namespace Ride.Infra.Database;

using System.Threading.Tasks;

public interface IConnection
{
    Task<IEnumerable<object>> Query(string statement, object data);
    Task Close();
}
