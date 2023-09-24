namespace Ride.Infra.Gateway;

public sealed class MailerGateway
{
    public MailerGateway() { }

    public async Task Send(string email, string subject, string message)
    {
        Console.WriteLine($"{email}, {subject}, {message}");
    }
}
