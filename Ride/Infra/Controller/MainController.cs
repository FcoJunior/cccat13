using Ride.Application.UseCase;
using Ride.Infra.Http;
using HttpMethod = Ride.Infra.Http.HttpMethod;

namespace Ride.Infra.Controller;

public sealed class MainController
{
    public MainController(IHttpServer httpServer, Signup signup, GetAccount getAccount)
    {
        httpServer.On(HttpMethod.Post, "/signup", signup.Execute);
        httpServer.On(HttpMethod.Get, "/accounts/{accountId}", getAccount.Execute);
    }
}