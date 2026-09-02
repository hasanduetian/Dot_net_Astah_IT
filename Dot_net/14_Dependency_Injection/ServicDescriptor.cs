public enum ServiceLifetime
{
    Transient,
    Scoped,
    Singleton
}
public class ServiceDescriptor(Type serviceType,Type implementationType, ServiceLifetime lifetime)
{
    public Type ServiceType{get;}=serviceType;
    public Type ImplementationType{get;}=implementationType;
    public ServiceLifetime Lifetime{get;}=lifetime;

    public object? SingletonInstance{get; set;} // for singleton instance
    public object SingletonLock{get;}=new(); // for singleton instance lock
}