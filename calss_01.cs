using System;
using System.Data.Common;
using System.Runtime.CompilerServices;


// Encapsulation --> combine of field , propertics and method. below example is an enharitance
class BankAccount
{
    private decimal _balance; // private field jeta ami kwke access dibo na setar name er age _ thalbe
    public decimal Balance // full properties
    {
        get{return _balance;}
        private set
        {
            if(value<0) // value means balance value
            throw new ArgumentException("balance can not be negative");
            _balance=value;
        }
    }

    public void Deposit(decimal amount)
    {
        if(amount<=0)
            throw new ArgumentException("Balance must be positive");
        Balance+=amount;
    }
}

// init properties-------------------
public class Employee
{
    public int Id{get;set;} // auto_implemented properties
   // public string? Department{ get; init; } // ? --> means stirng default nullable--> eita null value accept kore  -- kno vlaue na dilew hobe
    public string Department{ get; init; }=string.Empty;// or "hasan";      // --> it's non Nullable..... thats why there assign a value
     // init means after first time assignment of department it can never be changed like constant
    
    private String _email=string.Empty;
    public string Email
    {
        get => _email; // => means expression body syntax
        set
        {
            if(!value.Contains("@"))
                throw new FormatException("Invalid Email");
            _email=value;
        }
    }

}

// Data validation------------------------
public class Rectangle{
    public double Width{get; set;}
    public double Height{get; set;}
    public double Area => Width*Height; // area is also a field
    private double _Width;
    public double ValidWidth
    {
        get =>_Width;
        set
        {
            if(value<=0)
            throw new ArgumentException("width must be > 0");
            _Width=value;
        }
        
    }
}
// Inheritance ----------------------
public class Vehicle
{
    public string Brand{get; set;}=string.Empty;
    public void Start() => Console.WriteLine($"{Brand} Vehicle starting .. "); // string Interpoletion
}
public class Car:Vehicle
{
    public int TrunkCapacity{get; set;}
    
}
/// another inheritance --------------------------------------
public class Person
{
    public string Name{get; set;}
    public Person(string name) => Name=name;
    public virtual void Introduce() => Console.WriteLine($"I am {Name}");
}

public class Student : Person
{
    public string Major{get; set;}
    public Student(string name, string major):base(name){
        Major=major;
    }
    public override void Introduce()
    {
        base.Introduce();
        Console.WriteLine($"I Study {Major}");
    }
}

// Method hidding and overiding-------------------------------------------
public class Animal
{
    public virtual void Speak() => Console.WriteLine("Animal noise"); // for this virtual keyword drive class can modify this method as their wish
    public void Move() => Console.WriteLine("Animal move");  
}
public class Dog : Animal
{
    public override void Speak() => Console.WriteLine("Dog barks"); // for modify method of base class we use overrided 
    public new void Move() =>Console.WriteLine("dog runs"); // new key word use for data hiddign--> this method only belong this class
        
}

// protected -------------------------------------------------
public class Account // protected is accesabel only it's own calss and derived calss not other
{
    protected decimal Balance{get; set;}
    public Account(decimal InitialBalance)
    {
        Balance=InitialBalance;
    }
    protected void LogTransaction(string msg) => Console.WriteLine($"Log {msg}");
}
public class SavingsAccount : Account
{
    public SavingsAccount(decimal InitialBalance):base(InitialBalance){ }
    public void AddInterest(decimal rate)
    {
        decimal interest=Balance*rate;
        Balance+=interest;
        LogTransaction($"Interest added: {interest}");
    }
}

// composition ---> has a relation
public class Engine
{
    public void Start()=>Console.WriteLine("Engine started");
}
public class Cars
{
    private readonly Engine _engine=new Engine(); // composition--> cars inherite engine
    public void Start()
    {
        _engine.Start();
        Console.WriteLine("Car is ready to go");
    }
}


    class Program
    {
        public static void Main(){
            Employee e = new Employee { Id=1, Department="CSE" };
            //e.Department="economic"; --> it show error because init get the just first assign value
            Console.WriteLine(e.Department);

            // Inheritance
            Car c=new Car();
            c.Brand="BMW";
            c.Start();

            Student s=new Student("hasan","cse");
            s.Introduce();

            // Data overriding and data hidding 
            Animal a  = new Dog(); // animal object intialize with dog calss --> animal er kase obect ase dog er
            a.Speak();
            a.Move(); // that is not show dog move it show animal move because new keyword hidding the dog data thats why it show pareant class data

            Dog d = new Dog();
            d.Speak();
            d.Move();

            // protected--------------------------
            SavingsAccount sa=new SavingsAccount(10000);
            //sa.Balance=1000;//not accessable due to protected;
            sa.AddInterest(0.05m);
            //Console.WriteLine(sa.Balance); // this is also not acces able becase of protectd method
            
            // composition
            Cars ca=new Cars();
            ca.Start();
        }
    }
