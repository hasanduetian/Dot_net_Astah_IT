
public interface ITransientService
{
    Guid Id {get;}
}
public class TransientService:ITransientService
{
    // jwkon akta object crete hobe twkon akta id create hobe.
    public Guid Id {get;} =Guid.NewGuid(); // every time new instance will be created
}
public interface IScopedService
{
    Guid Id {get;}
}
public class ScopedService:IScopedService
{
    public Guid Id {get;} = Guid.NewGuid(); // every time new instance will be created but within the same scope it will be same
}
public interface ISingletonService
{
    Guid Id {get;}
}
public class SingletonService : ISingletonService
{
    public Guid Id{get;}=Guid.NewGuid(); // 
}