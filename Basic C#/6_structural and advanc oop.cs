
using System;
class Program
{
    public static void Main()
    {
        // Adapter  Pattern
        var laptop=new Laptop();
        var europiunPlag=new EuropianPlag();
        var adapter=new SoketAdapter(europiunPlag);
        laptop.Charger(adapter);


        // Decorator Pattern
        ICoffe coffe=new SimpleCoffe();
        coffe=new MilkDecorator(coffe);
        coffe=new SugerDecorator(coffe);
        Console.WriteLine($"Coffe{coffe.GetDescription()} price {coffe.GetCost()}");        
    }
}

// Adapter Pattern----- Its allow incompatible interface work together . act as a bridge
public class EuropianPlag // Existing class with incompatible interface
{
    public string GetEuropianSoket()
    {
        return "Europian Soket";
    }
}
public interface IUSASoket // Target interface expected by client
{
    string GetUSASoket();
}
public class SoketAdapter:IUSASoket  // Adapter class
{
    private readonly EuropianPlag _europianPlag;
    public SoketAdapter(EuropianPlag europianPlag)
    {
        _europianPlag=europianPlag;
    }
    public string GetUSASoket()
    {
        return $"adapter converterting {_europianPlag.GetEuropianSoket()}";
    }
}
public class Laptop
{
    public void Charger(IUSASoket soket)
    {
        Console.WriteLine($"Charging Using {soket.GetUSASoket()}");
    }
}




// Decorator Pattern----------------
// Component interface
public interface ICoffe
{
    string GetDescription();
    double GetCost();
}
// Concrete component
public class SimpleCoffe : ICoffe
{
    public string GetDescription()=>"simple coffe";
    public double GetCost()=>10.00;
}
// Base decorator
public abstract class CoffeDecorator : ICoffe
{
    protected readonly ICoffe _coffe;
    protected CoffeDecorator(ICoffe coffe)
    {
        _coffe=coffe;
    }
     public virtual string GetDescription()=> _coffe.GetDescription();
    public virtual double GetCost()=>_coffe.GetCost();
}

// concreat decorators
public class MilkDecorator : CoffeDecorator
{
    public MilkDecorator(ICoffe coffe):base(coffe){}
    // public override string GetDescription()
    // {
    //     return base.GetDescription()+" Milk";
    // }  
    // or
    public override string GetDescription() => _coffe.GetDescription()+" Milk";
    // public override double GetCost()
    // {
    //     return base.GetCost()+2.00;
    // }
    //or
    public override double GetCost()=> _coffe.GetCost()+2.00;
    
}
public class SugerDecorator : CoffeDecorator
{
    public SugerDecorator(ICoffe coffe):base(coffe){}
    public override string GetDescription()
    {
        return base.GetDescription()+" Suger";
    }
    public override double GetCost()
    {
        return base.GetCost()+5.00;
    }
}

// Repository pattern-------it is user for access or communicate database database -------------------------
// Domain entity
public class Product // in database it is tabel name
{
    public int Id{get; set;} // column nam e
    public string Name{get; set;}
    public decimal Price{get; set;}
}
// Repository interface (abstraction) // database phase
public interface IProductRepository
{
    Task<Product>GetByIdAsync(int id);
    Task<IEnumerable<Product>>GetAllAsync();
    Task<Product>AddAsync(Product product);
    Task UpdateAsync(Product product);
    Task DeleteAsync(int id);
}
// concreate implementation // dabase phase
public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _context;
    public ProductRepository(AppDbContext context)
    {
        _context=context;
    }
    public async Task<Product>GetProductAsync(int id)=> 
    await _context.Prodructs.FindAsync(id);
    public async Task<IEnumerable<Product>>GetAllAsync()=>
    await _context.Products.ToListAsync();
    public async Task<Product>AddAsync(Product product)
    {
        _context.Products.Add(product);
        await _context.SaveChangeAsync();
        return product;
    }
    // Update and delete async follow same pattern
} 

//Service layer using repository // business logic
public class ProductService
{
    private readonly IProductRepository _repository;
    public ProductService(IProductRepository repository)
    {
        _repository=repository;
    }
    public async Task<Product>CreateProductAsync(string name,decimal price) // create a new product
    {
        var product=new Product
        {
            Name=name,
            Price=price
        };
        return await _repository.AddAsync(product);
    }
} 


// Asynchronous Programming ---
// Basic async method
public class AsynchronoysProgram
{
    public async Task<string> DownloadDataAsync(string url) // for use thread use async keyword and use task
    {
        using var client=new HttpClient();
        // Thread released here while waiting--> await release the thread
        var result=await client.GetStringAsync(url);
        return result;
    }

    // Parallel execution (when tasks are independent)
    public async Task ProcessMultipleAsync()
    {
        var task1=FeatchDataAsync(); //starts immediately
        var task2=FeatchOtherAsync();// starts immediately
        var result=await Task.WhenAll(task1,task2); // it release all thread which are occupied
    }

    //Anti-Patterns
    // Anti-pattern: async void (BAD)
    public async void BadAsync() {} // Avoid!
    // Good: Always return Task
    public async Task GoodAsync() {}
}
