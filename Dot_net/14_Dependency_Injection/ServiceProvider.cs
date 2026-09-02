using System;
using System.Collections.Generic;
using System.Linq;

public class ServiceProvider
{
    private readonly IReadOnlyList<ServiceDescriptor> _services;
    private readonly Dictionary<Type, object>? _scopedCache; // for scoped instance cache

    public ServiceProvider(IReadOnlyList<ServiceDescriptor> services) // for root service provider we don't need to create a scoped cache
    {
        _services = services;
    }
    public ServiceProvider(IReadOnlyList<ServiceDescriptor> services,bool isScope) // if it is a scope then we need to create a scoped cache
    {
        _services = services;
        if (isScope)
        {
            _scopedCache=new Dictionary<Type, object>(); // initialize scoped cache if it is a scope
        }
    }


    public T GetRequiredService<T>()
    {
        return (T)GetService(typeof(T));
    }

    public object GetService(Type serviceType)
    {
        var descriptor = _services.FirstOrDefault(x => x.ServiceType == serviceType)  // check if service is registered or not
            ?? throw new Exception($"Service of type {serviceType.Name} is not registered"); // if not registered

        return descriptor.Lifetime switch  // else
        {
            ServiceLifetime.Transient => CreateInstance(descriptor.ImplementationType),
            ServiceLifetime.Scoped => CreateScopedInstance(descriptor),
             ServiceLifetime.Singleton => CreateSingletonInstance(descriptor),
            _ => throw new NotImplementedException()
        };
    }

    public ServiceScope CreateScope()
    {
        var scopeProvider=new ServiceProvider(_services,true); // create new service provider for scope
        return new ServiceScope(scopeProvider); // return new scope with service provider
    }

    public object CreateScopedInstance(ServiceDescriptor descriptor)
    {
        if(_scopedCache==null)  // if is it a root service provider then throw exception because we cannot create scoped instance from root service provider
            throw new InvalidOperationException("Cannot resolve scoped service from root provider");  //if it is not a scope then throw exception
    
        if(_scopedCache.TryGetValue(descriptor.ServiceType,out var instance)) return instance; // check if instance is already created then return it
        instance=CreateInstance(descriptor.ImplementationType); // else create new instance
        _scopedCache[descriptor.ServiceType]=instance; // add new instance to cache
        return instance;
    
    }
    public object CreateSingletonInstance(ServiceDescriptor descriptor)
    {
        // if(descriptor.SingletonInstance==null) // check if singleton instance is already created or not
        // {
        //     var instance=CreateInstance(descriptor.ImplementationType);
        //     descriptor.SingletonInstance=instance;

        // }
        lock(descriptor.SingletonLock){ // if multiple threads are trying to create singleton instance at the same time then lock will ensure that only one thread can create the instance at a time
        descriptor.SingletonInstance ??=CreateInstance(descriptor.ImplementationType); // if singleton instance is not created then create it   
        return descriptor.SingletonInstance;
        }
    }
    public object CreateInstance(Type implementationType) 
    {
        var ctor = implementationType.GetConstructors().FirstOrDefault()  // get the first constructor of the implementation type
            ?? throw new Exception($"No constructor found for type {implementationType.Name}"); // if no constructor found

        var dependencies = ctor.GetParameters() // check if the constructor has parameters or not
            .Select(x => GetService(x.ParameterType)) // get the service of the parameter type
            .ToArray(); // convert to array

        return Activator.CreateInstance(implementationType, dependencies); // create an instance of the implementation type with the dependencies
    }

    internal void DisposeScopedInstance() // dispose all the instances in the scoped cache
    {
        if(_scopedCache==null) return; // if it is not a scope then return
        foreach(var instance in _scopedCache.Values) // if it is a scope then dispose all the instances in the cache
        {
            if(instance is IDisposable disposable) disposable.Dispose(); // if the instance is disposable then dispose it
        }
        _scopedCache.Clear(); // clear the cache
    }
}
