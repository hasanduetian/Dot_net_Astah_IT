public interface IServiceScope:IDisposable  // scope is dispose able
{
    ServiceProvider ServiceProvider{get;} // scope er moddhe service provider thakbe
}
public class ServiceScope : IServiceScope
{
    public ServiceProvider _serviceProvider;
    public ServiceScope(ServiceProvider serviceProvider)  // scope er moddhe service provider thakbe
    {
        _serviceProvider = serviceProvider;
    }
    public ServiceProvider ServiceProvider => _serviceProvider;  // sevrvice provider er modhe instance pathaye deya

    public void Dispose()
    {
        _serviceProvider.DisposeScopedInstance(); // scope er moddhe service provider er instance dispose korbe
    }
}