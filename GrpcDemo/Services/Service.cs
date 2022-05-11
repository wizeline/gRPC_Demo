using System.Diagnostics;
using Grpc.Core;

namespace GrpcDemo.Services;

public class Service: Greeter.GreeterBase
{
    private readonly ILogger<Service> _logger;

    public Service(ILogger<Service> logger)
    {
        _logger = logger;
        
        var process = Process.GetCurrentProcess();
        var processInfo = $"Id:{process.Id}-Name:{process.ProcessName}-Handle:{process.Handle}";
        _logger.LogInformation($"@@ Service starting process:{processInfo}\n");
    }

    public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
    {
        _logger.LogInformation($"@@ Service: Saying hello to {request.Name}\n");
        return Task.FromResult(new HelloReply()
        {
            Message = "Hello " + request.Name
        });
    }
}