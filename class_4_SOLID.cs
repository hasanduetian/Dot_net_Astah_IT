using System;
using System.ComponentModel.DataAnnotations;
public class Program
{
    public static void Main()
    {
        // open closed principle
        var claculator = new AreaCalculator();
        var rectangle=new Rectangle{Height=4.0, Width=10.0};
        var circle = new Circle{Radius=3.00};
        Console.WriteLine(claculator.CalculateArea(rectangle));
        Console.WriteLine(claculator.CalculateArea(circle));
    
        // dependecy inversion principle
        var services= new OrderService(new SqlDatabase());
        var servicess= new OrderService(new MongoDatabase());
    
    }
    // safe substitution --only flyable birds
    public void MakeBirdFly(IFlyable brid) // its only run for flyable birds
    {
        brid.Fly();
    }
}

// SOLID PRINCIPLE
// S--> Single responsibility Principle--> single class responsible for one work not One class doing everything: data storage, reporting, and email.
public class Employee
{
    public string? Name{get; set;}
    public decimal Salary{get; set;}

}
public class EmployeeRepository// Its a pattern which through communicate with databse
{
    public void SaveToDatabase(Employee e){}
}
public class EmployeeReportGenerator
{
    public void GenerateReport(Employee e){}
}
public class EmployeeEmailService
{
    public void SendEmail(Employee e){}
}


// O---> open closed principle
//Software entities should be open for extension but closed for modification. Add new behavior without changing existing code.
// violation ocp( bad example)

// public class Rectangle
// {
//     public decimal width{get; set;}
//     public decimal height{get; set;};
    
// }
// public class Circle
// {
//     public double radus{get;set;}
// }
// public class AreaCalculate 
// {
//     public double CalculateArea(object shape)// object is a datatype which contain different type of class object
//     {
//         if(shape is Rectangle)
//         {
//             var r=(Rectangle)shape;
//             return r.width*r.height;
//         }
//         else if(shape is Circle)
//         {
//             var c=(Circle)shape;
//             return Math.PI*c.radus*c.radus;
//         }
//         return 0;
//     }
//     // here we change or modify the existing code or modify this 
// } // so it violate the open closed principle


// with open closed principle--------------
public interface Ishape
{
    double CalculateArea();
}
public class Rectangle:Ishape
{
    public double Width{get; set;}
    public double Height{get; set;}
    public double CalculateArea() => Width*Height;
}
public class Circle:Ishape
{
    public double Radius{get; set;}
    public double CalculateArea()=>Math.PI*Radius*Radius;
}
// it dont hold single responsible principle
public class AreaCalculator
{
    public double CalculateArea(Ishape shape) => shape.CalculateArea();
}

//L---> liskov substitution principle
//Objects of a superclass shall be replaceable with objects of subclasses without affecting correctness. Derived classes must be
//substitutable for their base classes.

// bad example
// public class Bird
// {
// public virtual void Fly() { ... }
// }

// public class Ostrich : Bird
// {
// // Violation: Ostrich can't fly!
// public override void Fly()
// {
// throw new InvalidOperationException("Ostrich cannot fly!");
// }
// }

/// good example-------------------- paraent class or child class both are replace each othe object 
public interface IFlyable
{
    void Fly();
}
public interface IRunable
{
    void Run();
}
//creter behavior alada Interface create
public class Sparrow:IFlyable
{
    public void Fly(){}
    public void Run(){}

}
public class Ostrich : IRunable
{
    public void Run(){ }
}

// I---> Interface segregation principle
// Clients should not be forced to depend on interfaces they do not use. Split large interfaces into smaller, focused ones.

// Bad example: A Robot is forced to implement Eat() and Sleep() it doesn't need.

// public interface IWorker
// {
//     void Work();
//     void Eat();
//     void Sleep();
// }
// public class Robot : IWorker
// {
// public void Work() { }
// // Violation: Robot doesn't need these!
// public void Eat() { /* throw */ }
// public void Sleep() { /* throw */ }
// }


// good example
// jar je behavior sita inharit kore kaj korbe
public interface IWorkable
{
    void Work();
}
public interface IFeedable
{
    void Eat();
}
public interface ISleepable
{
    void Sleep();
}
public class Human : IWorkable, IFeedable, ISleepable
{
    public void Work(){}
    public void Eat(){}
    public void Sleep(){}
}
public class Robot : IWorkable
{
    public void Work(){}
}


// D--> Dependency Inversion Principle
// High-level modules should not depend on low-level modules; both should depend on abstractions.
// Depend on abstractions, not concretions.

// Bad example--OrderService is tightly coupled to SqlDatabase.

// public class OrderService
// {
// private readonly SqlDatabase _database;

// public OrderService()
// {
// _database = new SqlDatabase(); // Tight coupling!
// }

// public void SaveOrder(Order order)
// {
// _database.Save(order);
// }
// }

// good example---------------
public interface IDatabase
{
    void Save(object entity);
}
public class SqlDatabase : IDatabase
{
    public void Save(object e){}
}
public class MongoDatabase : IDatabase
{
    public void Save(object e){}
}

// high-level module depend on abstraction not depend on low level  module 
public class OrderService
{
    private readonly IDatabase _database;
    public OrderService(IDatabase database)
    {
        _database=database;  // Dependecy Injection
    }
    public void SaveOrder (Order order)
    {
        _database.Save(order);
    }
}

// A proper example for all principle
// E-Commerce Order Processing: Before Refactoring
// public class OrderProcessor
// {
// public void Process(Order order)
// {
// // Validate
// if (order.Items.Count == 0) throw ...

// // Check inventory
// if (!Inventory.InStock(order)) throw ...

// // Process payment (hard-coded!)
// if (order.PaymentType == "Credit") { ... }
// else if (order.PaymentType == "PayPal") { ... }

// // Save to DB
// new SqlDatabase().Save(order);

// // Send email
// new EmailService().Send(order);
// }
// }

// refactorign step by step
// clean maintaable code structure
public interface IOrderValidator
{
    ValidatorResult Process (Order order);
}
public interface IPaymentProcessor
{
    PaymentResult Process(Order order);
}
public interface IInventoryService
{
    bool CheckAvailability(Order order)
}
public interface INotificationService
{
    void SendConformation(Order order);
}

public class OrderProcess
{
    private readonly IOrderValidator _validator;
    private readonly IPaymentProcessor _payment;
    private readonly IInventoryService _inventory;
    private readonly INotificationService _notify;

    public OrderProcess(
        IOrderValidator validator,
        IPaymentProcessor payment,
        IInventoryService inventor,
        INotificationService notify
    ){
        _validator=validator;
        _payment=payment;
        _inventory=inventor;
        _notify=notify;
    }
    public void Process(Order order)
    {
        _Validator.validator (order);
        _inventory.CheckAvailability(order);
        _payment.Process(order);
        _notify.SendConformation(order);
    }
    
}










