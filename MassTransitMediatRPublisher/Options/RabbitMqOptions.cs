namespace MassTransitMediatRPublisher.Options;

public class RabbitMqOptions
{
    public string[] HostNames { get; set; }
    public string VirtualHost { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }

    public string? MainHost
    {
        get
        {
            if (HostNames == null) return null;

            return HostNames.FirstOrDefault();
        }
    }
}
