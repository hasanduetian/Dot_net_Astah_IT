using System;
class Program
{
    public static void Main(){
        // Cling program

        // factory method pattern -------------------
        // for email notification
        NotificationFactory factory= new EmailFactory();
        INotification notification=factory.CreateNotification();
        notification.Send();
        // for sms notification
        NotificationFactory smsfactory=new SmsFactory();
        INotification notify=smsfactory.CreateNotification();
        notify.Send();

        // abstrack method pattern
        IGUIFactory btnfactoyr=new WinFactory();
        IButton button= btnfactoyr.CreateButton();
        button.Paint();
        ICheckbox checkbox= new WinCheckbox();
        checkbox.Paint();
        
        // Builder pattern--------------------------------
        var pc=new Computer.Builder()   // method chaining
        .WithCPU("Intenl")
        .WithRAM(120)
        .WithStorage(520)
        .WithGPU(true)
        .Build();

        // observer pattern--- je je subscribe korbe tara shud notification pabe otherwise kew pabe na
        var agency=new NewsAgency();

        var channel1=new NewsChannel();
        var channel2=new NewsChannel();
        var channel3=new NewsChannel();
        
        agency.Subscribe(channel1);
        agency.Subscribe(channel2);
        agency.Subscribe(channel3);

        agency.SetNews("this is behavarial pattern");

        agency.UnSubscribe(channel2);
        agency.SetNews("delete behavarial pattern");

        /// stratygy Pattern -----------------
        var card = new ShoppingCard();
        card.SetPyamentStratygy(new CraditCardPayment());
        card.Checkout(500);
        card.SetPyamentStratygy(new PayPalPayment());
        card.Checkout(100);

    }
    
}

// Factory Method Pattern --- > Creates objects through a factory-----creational design pattarn that create object without exposing the ecact class(new keyword) to the cling
// Product Interface
public interface INotification // crete a common interface
{
    void Send();
}
// Concrete Products
partial class EmailNotification : INotification
{
    public void Send()=> Console.WriteLine("Sending Email Notification");
}
partial class SmsNotification : INotification
{
    public void Send()=> Console.WriteLine("Sending Sms Notification");
}
// crate a factoy of notification
public abstract class NotificationFactory
{
    public abstract INotification CreateNotification();
}
public class EmailFactory : NotificationFactory // Email factory for object creation 
{
    public override INotification CreateNotification(){ return new EmailNotification();}
    
}
public class SmsFactory : NotificationFactory // smsfactory for objct cretion
{
    public override INotification CreateNotification() { return new SmsNotification();}
    
}


// Abstrac t factory method---->Create related objects together.
public interface IButton{void Paint();}
public interface ICheckbox{void Paint();}
public class WinButton : IButton
{
    public void Paint()=>Console.WriteLine("windown button");
}
public class WinCheckbox : ICheckbox
{
    public void Paint()=>Console.WriteLine("windown checkbox");
}
public class MacButton : IButton
{
    public void Paint()=>Console.WriteLine("Macdown button");
}
public class MacChecbox : ICheckbox

{
    public void Paint()=>Console.WriteLine("Macdown Checkbox");
}
public interface IGUIFactory // Creates families of related objects

{
    IButton CreateButton();
    ICheckbox CreateCheckbox();

}
public class WinFactory : IGUIFactory
{
    public IButton CreateButton()=> new WinButton();
    public ICheckbox CreateCheckbox()=> new MacChecbox();
}
public class MacFactory : IGUIFactory
{
    public IButton CreateButton()=> new MacButton();
    public ICheckbox CreateCheckbox()=> new MacChecbox();
}


// Builder Pattern  ---------------------------------------------------------------------------------
// constract Complex object step by step ---------- The main goal is to create a complex Computer object step by step while keeping the Computer class immutable
public class Computer
{
    public string CPU {get;}
    public int RAM {get;}
    public int Storage{get;}
    public bool HasGPU{get;}

    private Computer (Builder builder) // private constructor --> which is not accesable in outside
    {  
        CPU=builder.CPU;
        RAM=builder.RAM;
        Storage=builder.Storage;
        HasGPU=builder.HasGPU;
    }
    
    // internal class
    public class Builder  // so we create a builder class to acess the private constructor
    {                    // which is carbon copy for computer class
    public string CPU {get;private set;}
    public int RAM {get;private set;}
    public int Storage{get;private set;}
    public bool HasGPU{get;private set;}

    public Builder WithCPU(string cpu) // its return an objects
        {
            CPU=cpu;
            return this;
        }
    public Builder WithRAM(int ram) // it's also return an object
        {
            RAM=ram;
            return this;
        }
    public Builder WithStorage(int strorage)
        {
            Storage=strorage;
            return this;
        }
    public Builder WithGPU(bool gpu)
        {
            HasGPU=gpu;
            return this;
        }
    public Computer Build() => new Computer(this);
    }
}



/// Observer Pattern----Defines a one-to-many dependency between objects. When one object changes state, all its dependents are notified automatically.
/// channel notify korle jara jara subscribe korche tara notification pabe jara kore nai tara pabe na 
public interface IObserver
{
    void Update(string message);
}
public class NewsChannel : IObserver
{
    public void Update(string message) => Console.WriteLine($"Breaking: {message}");
}
public class NewsAgency
{
    private readonly List<IObserver> _observers=new();
    public void Subscribe(IObserver observer) => _observers.Add(observer);
    public void UnSubscribe(IObserver observer) => _observers.Remove(observer);

    public void SetNews(string news)
    {
        foreach(var observer in _observers)
        {
            observer.Update(news);
        }
    }
}


// Strategy pattern---> Interchangeabe algorithm in runtime
public interface IPaymentStratygy
{
    void Pay(int amount);
}
public class CraditCardPayment : IPaymentStratygy
{
    public void Pay(int amount)=> Console.WriteLine($"paid {amount} via Cradit Card");
}

public class PayPalPayment : IPaymentStratygy
{
    public void Pay(int amount)=> Console.WriteLine($"paid {amount} via Paypal");
}
public class ShoppingCard
{
    private IPaymentStratygy _stratygy;  // create a references of interface
    public void SetPyamentStratygy(IPaymentStratygy stratygy) =>_stratygy=stratygy;
    public void Checkout(int amount)=>_stratygy.Pay(amount);
}























