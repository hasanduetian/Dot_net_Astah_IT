using System;
using System.Reflection;
class Program{

// delegates methods-----------
    static int Add (int a,int b)=>a+b; // static deya lagbe karon bydefult internal thake 
    static int Multiply (int a,int b)=>a+b;

// Action methods---it's don't allow return type--------------
    static void GreetMethod(string name,string dep)
    {
        Console.WriteLine($"Hello {name} and dep {dep}");
    }

// Function ---its allow return type
    static double SubstracMethod(int a,int b)
    {
        return a/(double)b;
    }

// Predicate----it accept one parameter and return a bool 
    static bool IsEvenMethod(int n)
    {
        return n%2==0;
    }

// Event---Events wrap delegates to prevent direct invocation from outside the class. They provide a clean publish-subscribe mechanism
// where the publisher controls when notifications are sent.---
    static void AlertTemparatureHandler(double temp)
    {
        Console.WriteLine($"Alert: {temp}C");
    }

    static void Main(){
        
// reflection :--------------------------------------------------------------------------
        Type type = typeof(DateTime); // we are get this line from unknown source ,so we can know this type
        Console.WriteLine($"Name: {type.Name}"); 
        Console.WriteLine($"Name: {type.Namespace}");
      
        // foreach(var method in type.GetMethods())
        // {
        //     Console.WriteLine(method.Name);
        // }


// Dynamic object creation -----------------------------------------------------------
      
        // Get the Type object representing List<string>
        Type t=typeof(List<string>);
        // Dynamically create an instance of that type at runtime
        object instance=Activator.CreateInstance(t);
        // Cast the untyped object back to List<string> to use it normally
        var list=(List<string>)instance;
        // then simply work through it
        list.Add("2");
        list.Add("3");
        list.Add("4");
        list.Add("5");

        Console.WriteLine(list[3]);


// Reflection of Attribute---Reading at runtime----------------------------------------
        var attr=typeof(Calculator).GetCustomAttribute<DeveloperAttribute>();
        Console.WriteLine(attr.Name);
        Console.WriteLine(attr.Version);

// delegate-------------------------
        Calculate cal=Add;
        Console.WriteLine(cal(3,4));
        cal=Multiply;
        Console.WriteLine(cal(3,4));

// Action --------------------
        Action<string,string>g=GreetMethod; // action void return kore and eita just parameter ney
        g("Hasan","cse");        

// fuction -------------------
        Func<int, int,double >sub=SubstracMethod; // func accept return type 
        Console.WriteLine(sub(8,5));

// Predicates---------------------
        Predicate<int>isEven=IsEvenMethod;
        List<int>numbers=new(){1,2,3,4,5,6};
        var evenNumbers=numbers.FindAll(isEven);
        foreach(var num in evenNumbers)
        {
            Console.Write(num+" ");
        }
// Events------------------------------
        var sensor=new TemperatureSensor();
        sensor.TemperatureChanged+=AlertTemparatureHandler;
        sensor.Temperature=25.00;
        sensor.Temperature=30.3;

// pub-sub----multiple subscriber
        var publisher=new NewsPublisher();
        publisher.NewsPublished += msg =>Console.WriteLine($"Email sent: {msg}"); // inline method same as method
        publisher.NewsPublished += msg1 =>Console.WriteLine($"Sms sent: {msg1}");
        publisher.Publish("c++ class is started ");
    }
}


// Attributes - Adding Metadata
//Attributes attach declarative information to classes, methods, or properties. They are a powerful way to embed metadata
//directly into your code that can be inspected at runtime via Reflection.

[Developer("Hasan",Version=1)]
//[Developer(Name= "Hasan",Version=1)]
class Calculator
{
}
public class DeveloperAttribute : Attribute
{
    public string Name{get;}
    public int Version {get; set;}
    public DeveloperAttribute(string name)
    {
        Name=name;
    }
}

// Delegate--A delegate is a type that references a method with a specific signature. It enables methods to be passed as parameters,stored in variables, and invoked dynamically.--------------------------
public delegate int Calculate(int x, int y);


// Aciton --No need to declare custom delegates every time. .NET provides Action and Func as generic, ready-to-use delegate types.

// Events - loose coupling with delegates
public class TemperatureSensor
{
    public event Action<double>TemperatureChanged;
    private double _temperature;
    public double Temperature
    {
        get=> _temperature;
        set
        {
            _temperature=value;
            TemperatureChanged?.Invoke(value); // generally it's call the method : TemperatureChanged
        }
    }
}

// publisher subscriber pattern
public class NewsPublisher
{
    public event Action<string>NewsPublished;
    public void Publish(string news)
    {
        Console.WriteLine("Publishing: "+news);
        NewsPublished?.Invoke(news); // call the action methods
    }
}

