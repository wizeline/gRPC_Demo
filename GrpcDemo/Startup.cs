using GrpcDemo.Services;

namespace GrpcDemo;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddGrpc();
    }

    public void Configure(IApplicationBuilder app)
    {
        app.UseRouting();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapGet("/", async context =>
            {
                await context.Response.WriteAsync("Starting gRPC Demo.\n Look at the console output for details ...\n");
            });
            endpoints.MapGrpcService<Service>();
        });
    }
}