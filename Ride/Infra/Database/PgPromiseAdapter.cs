using System.Data;
using Npgsql;
using Dapper;

namespace Ride.Infra.Database;

public class PgPromiseAdapter : IConnection
{
    private readonly IDbConnection _connection;

    public PgPromiseAdapter()
    {
        _connection = new NpgsqlConnection("Host=silly.db.elephantsql.com;Port=5432;Pooling=true;Database=xezcsitl;User Id=xezcsitl;Password=POOfyLgnX0LvEhazmtOmyXV6_HH-899a;");
    }

    public async Task<IEnumerable<object>> Query(string statement, object data)
    {
        return await _connection.QueryAsync(statement, data);
    }

    public async Task Close()
    {
        if (_connection.State != ConnectionState.Closed)
        {
            _connection.Close();
        }
    }
}
