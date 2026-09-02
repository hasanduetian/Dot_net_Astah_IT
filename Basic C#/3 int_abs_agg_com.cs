using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Hasan
{
    public class Program
    {
        public static void Main()
        {
            var email = new EmailChannel();
            var sms = new SmsChannel();
            var discord = new DiscordChannel();
            var whatsapp = new WhatsAppChannel();

            var channels = new List<INotificationChannel> // list type
            {
                email,
                sms,
                discord,
                whatsapp
            };

            var service = new NotificationService(channels);

            service.NotifyAll("System recovered", "user01");

            
// method overloading -----------------------
              var mtov=new MethodOverloadding();
              mtov.Add(1,2,3,4,5,6);
              mtov.Add(1,2);
              mtov.Add("Hasan"," + roksana");

             // method overriding -------------------------
              Employee dev=new Developer // create object like this
              {
                  Name="hasan",
                  BaseSalary=5000,
                  Bonus=100
              };
              Console.WriteLine($"{dev.Name} : {dev.CalculateSalary()}");
              dev.Work();

              Employee manager=new Manager
              {
                  Name="Mahamud",
                  BaseSalary=8000,
                  TeamBonus=500
              };
              Console.WriteLine($"{manager.Name} : {manager.CalculateSalary()}");
              manager.Work();   

            // abstract class
            PaymentProcessor paypal=new PaypalProcessor();
            Console.WriteLine(paypal.ProcessorName);
            paypal.ProcessPayment(100,"USD");

            // Interface;
            IAuthenticable auth=new User();
            auth.Login("Hasan","1234");
            auth.Logout();

            //Interface Segregation Principle
            ConsoleLogger logger=new ConsoleLogger();
            logger.Log("logger");

            var robot=new Robot();
            robot.Wrok();

            var human=new Human();
            human.Wrok();
            human.Eat();


            // Association
            var student=new Student{Name="hasan"};
            var Course=new Course();
            Course.Enroll(student);

            // aggrigation 
            var emp=new Employeee{Name="hasan"};
            var dept=new Department{Name="IT"};
            dept.Employeees.Add(emp);
        }
    }

    // Interface----it make a contract which is must be used for this inhart class
    public interface INotificationChannel
    {
        void Send(string message, string recipient);
    }

    public class EmailChannel : INotificationChannel // inherit interface
    {
        public void Send(string message, string recipient)
        {
            Console.WriteLine($"Email sent to {recipient}: {message}");
        }
    }

    public class SmsChannel : INotificationChannel // inherit interface
    {
        public void Send(string message, string recipient)
        {
            Console.WriteLine($"SMS sent to {recipient}: {message}");
        }
    }

    public class DiscordChannel : INotificationChannel
    {
        public void Send(string message, string recipient)
        {
            Console.WriteLine($"Discord message sent to {recipient}: {message}");
        }
    }

    public class WhatsAppChannel : INotificationChannel
    {
        public void Send(string message, string recipient)
        {
            Console.WriteLine($"WhatsApp message sent to {recipient}: {message}");
        }
    }

    public class NotificationService
    {
        private readonly List<INotificationChannel> _channels;

        public NotificationService(IEnumerable<INotificationChannel> channels)
        {
            _channels = channels.ToList();
        }

        public void NotifyAll(string message, string recipient)
        {
            foreach (var channel in _channels)
            {
                channel.Send(message, recipient);
            }
        }
    }
    

// polymorphysm------------------two type------------------------------------------
// compile time --> Method overloading-------------------------------------
public class MethodOverloadding
    {
        public void Add(int a,int b) { Console.WriteLine(a+b); }
        public void Add(int a,int b,int c) { Console.WriteLine(a+b); }
        public void Add(double a,double b) { Console.WriteLine(a+b); }
        public void Add( string a,string b) { Console.WriteLine(a+b);}
        public void Add (params int[] numbers){Console.WriteLine(numbers.Sum());} // value add as per your wish
    }

// run time polymortpysom---->method overriding --------------
public class Employee
    {
        public string Name{get; set;}=string.Empty;
        public decimal BaseSalary {get; set;}
        public virtual decimal CalculateSalary()
        {
            return BaseSalary;
        }
        public virtual void Work()
        {
            Console.WriteLine($"{Name} is working ");
        }
    }
public class Developer:Employee
    {
        public decimal Bonus{get;set;}
        public override decimal CalculateSalary()
        {
            return BaseSalary+Bonus;
        }
        public override void Work()
        {
            Console.WriteLine($"{Name} is implemetting code ");
        }
    }
public class Manager:Employee
    {
        public decimal TeamBonus{get;set;}
        public override decimal CalculateSalary()
        {
            return BaseSalary+TeamBonus;
        }
        public override void Work()
        {
            Console.WriteLine($"{Name} is Managing the team");
        }
    }


// abstruct class---------------------------------------------
public abstract class PaymentProcessor // abstract class ke inherit korle er method properties use kortei hobe
    {
        public void ValidAmount(decimal amount) // general method 
        {
            if(amount<=0)
            throw new ArgumentException("Amount must be positive ");
        }
        public abstract string ProcessorName{get;} // abstract properties
        public abstract void ProcessPayment(decimal amount,string currency); // abstract metthod --> don't be implemented here ..implimented in inherited classes
        public virtual void LogTransaction(string transactionId)
        {
            Console.WriteLine($"[{ProcessorName}] {transactionId}");
        }
    }
public class PaypalProcessor : PaymentProcessor
    {
        // public override string ProcessorName
        // {
        //     get
        //     {
        //         return "paypal";
        //     }
        // }
        public override string ProcessorName =>"paypal";
        public override void ProcessPayment(decimal amount, string currency)
        {
            ValidAmount(amount);
            Console.WriteLine($"Processing {amount} {currency} via Paypal sdk");
        }
        
    }


    /// interface-------------------------------------------------------------------
    public interface IAuthenticable // create conturct which is must be implemented in inherit class
    {
        bool Login(string username,string password);
        void Logout();
    }
    public class User : IAuthenticable
    {
        public string? Username{get; private set;}
        public bool Login(string username,string password)
        {
            if (password == "1234")
            {
                Username=username;
                Console.WriteLine($"{Username} logged in successfully.");
                return true;
            }
            Console.WriteLine("login failed");
            return false;
        }
        public void Logout()
        {
            Console.WriteLine("logout successfully ");
        }
        
    }

    // Interface Segregation principle---------------
    public interface IWrokable
    {
        void Wrok();
    }
    public interface IEatable
    {
        void Eat();
    }
    public interface ILogger
    {
        void Log(string message);
        void LogError(string error)=> Log($"[Error]:{error}"); // default implementation inherit class e implement na korlew hoy
    }
    public class ConsoleLogger : ILogger
    {
        public void Log(string message) => Console.WriteLine(message);
    }
    public class Robot : IWrokable
    {
        public void Wrok()=>Console.WriteLine("Robot is processing...");
    }
    public class Human : IWrokable, IEatable /// Interface er khetere multiple inheritance kora jay
    {
        public void Wrok() => Console.WriteLine("Human is coding...");
        public void Eat() => Console.WriteLine("Human is eating lunch.");
    }


    // Associatoin -------akta class er method er parmeter er modhe onno akta class er object ke pass kora
    public class Student
    {
        public string? Name{get;set;}
    }
    public class Course // course and student er modhe association
    {
        public void Enroll(Student student) // association method --that use class object
        {   
            Console.WriteLine($"{student.Name} enrolled");
        }
    }

    // Aggrigation ------------------has a relation ---->// akta part independent and arekta part dependent --> that is aggrigation  
    public class Employeee
    {
        public string? Name{get; set;}
    }
    public class Department
    {  
        public string? Name{get; set;}
        public List<Employeee>Employeees{get; set;}=new(); // if I remove this then it show error
    }


    // composition---------has a relation ---if both class dependent each other then it called compositon
    public class House  // akta arekta sara exist korte pare na
    {
        public List <Room> Rooms{get;}=new();
        public House()
        {
            Rooms.Add(new Room("living room"));
            Rooms.Add(new Room("kitchen room"));
        }
    }
    public class Room
    {
        public string Name{get;}
        public Room(string name)
        {
            Name=name;
        }
    }

}