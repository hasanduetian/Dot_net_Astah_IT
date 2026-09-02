// nijeder service collerction create korbo
public class CustomServiceCollection
{
    private readonly List<ServiceDescriptor> _services=[];  // register service gula ei list e store hobe

    public void AddTransient<TServiceType, TImplementationType>()
    {
        _services.Add(new ServiceDescriptor(
            typeof(TServiceType),
            typeof(TImplementationType),
            ServiceLifetime.Transient));
    }

    public void AddSingleton<TServiceType, TImplementationType>()
    {
        _services.Add(new ServiceDescriptor(
            typeof(TServiceType),
            typeof(TImplementationType),
            ServiceLifetime.Singleton));
    }
    public void AddScoped<TServiceType, TImplementationType>()
    {
        _services.Add(new ServiceDescriptor(
            typeof(TServiceType),
            typeof(TImplementationType),
            ServiceLifetime.Scoped));
    }
    

    public ServiceProvider BuildServiceProvider()
    {
        return new ServiceProvider(_services.AsReadOnly());
    }
}