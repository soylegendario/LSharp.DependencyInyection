# LSharp.DependencyInjection

The most easier way to inject your services.

## Getting Started

Download package from Nuget.
Decorate your services with `[Injectable]` specifying the injection type:

    [Injectable(ServiceLifetime.Transient)]
    public class MyPretty
    {
    }

Call to `AddInjectables` method from your Startup.cs file.

    public  void  ConfigureServices(IServiceCollection  services)
    {
	    ...
	    services.AddInjectables();
	    ...
    }

### Installing

Download LSharp.DependencyInjection from NuGet.

## License

This project is licensed under the MIT License.
