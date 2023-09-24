using Ride.Application.UseCase;
using Ride.Infra.Controller;
using Ride.Infra.Database;
using Ride.Infra.Http;
using Ride.Infra.Repository;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

IConnection connection = new PgPromiseAdapter();
AccountDaoDatabase accountDaoDatabase = new(connection);
Signup signup = new(accountDaoDatabase);
GetAccount getAccount = new(accountDaoDatabase);
IHttpServer httpServer = new MinimalApiAdapter(app);
new MainController(httpServer, signup, getAccount);
httpServer.Listen(3000);