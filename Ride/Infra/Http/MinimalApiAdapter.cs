namespace Ride.Infra.Http;

public sealed class MinimalApiAdapter : IHttpServer
{
    private readonly WebApplication _app;

    public MinimalApiAdapter(WebApplication app)
    {
        _app = app;
    }

    public void On(string method, string url, Delegate callback)
    {
        this._app.MapMethods(url, new[] { method }, callback);
    }

    public void Listen(int port)
    {
        _app.Run($"http://*:{port}");
    }
}