using System.Diagnostics;
using Grpc.Net.Client;

namespace GrpcDemo.Client;

public class Client: BackgroundService
{
    private readonly ILogger<Client> _logger;
    private readonly string _url;

    public Client(ILogger<Client> logger, IConfiguration configuration)
    {
        _logger = logger;
        _url = configuration["Kestrel:Endpoints:gRPC:Url"];
        
        var process = Process.GetCurrentProcess();
        var processInfo = $"Id:{process.Id}-Name:{process.ProcessName}-Handle:{process.Handle}";
        _logger.LogInformation($"## Client starting process:{processInfo}\n");
    }

    protected override async Task ExecuteAsync(CancellationToken ct)
    {
        using var channel = GrpcChannel.ForAddress(_url);
        var client = new Greeter.GreeterClient(channel);
        
        while (!ct.IsCancellationRequested)
        {
            var reply = await client.SayHelloAsync(new HelloRequest
            {
                Name = $"worker"
            });
            
            _logger.LogInformation($"##Client Greeting: {reply.Message} -- {DateTime.Now}\n");
            await Task.Delay(1000, ct); //wait 1 sec
        }
    }
}