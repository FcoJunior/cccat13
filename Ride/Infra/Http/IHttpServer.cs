namespace Ride.Infra.Http;

public interface IHttpServer
{
    void On(string method, string url, Delegate callback);
    void Listen(int port);
}

public static class HttpMethod
{
    public static readonly string Get = "GET";
    public static readonly string Post = "POST";
    public static readonly string Put = "PUT";
    public static readonly string Delete = "DELETE";
}
