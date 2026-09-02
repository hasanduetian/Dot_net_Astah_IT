using System;
namespace BusTicketBookingSystem
{
    public interface IDataStore // store all data in here
    {
        List<User>Users{get; }
        List<Bus>Buses{get; }
        List<Schedule>Schedules{get; }
        List<Ticket>Tickets{get; }
        List<Invoice>Invoices{get; }
    }
    public interface IBookingService
    {
        
    }
    public interface IBillingService
    {
        
    }
    
    public enum BusClassification
    {
        
    }
    public enum PaymentStatus
    {
        
    }

    public class User
    {
        public int UserId{get;}
        public string FullName{get; set;}
        public int MobileNumber {get; set;}
        public string Email{get; set;}
        public User(int userId,string fullName,int mobileNumber,string email)
        {
            UserId=userId;
            FullName=fullName;
            MobileNumber=mobileNumber;
            Email=email;
        }
    }

    public abstract class Bus
    {
        public int BusId{get;}
        public string CoachNumber{get;}
        public BusClassification Classification{get;}
        public int SeatingCapacity{get; protected set;}
        public List<string> SeatLayout{get;protected set;}=new List<string>();
        public Bus(int busId,string coachNumber,BusClassification classification)
        {
            BusId=busId;
            CoachNumber=coachNumber;
            Classification=classification;
        }
        public abstract bool IsValidSeat(string seatNumber);
    }
    public class BusinessBus
    {
        
    }
    public class EconmyBus{}
    public class Schedule{}
    public class Ticket{}
    public class Invoice{}









    class Program
    {
        public static void Main()
        {
        


        }
        
    }


}





































using System;
using System.Collections.Generic;
using System.Linq;

namespace BusTicketBookingSystem
{
    
    public interface IDataStore
    {
        List<User> Users { get; }
        List<Bus> Buses { get; }
        List<Schedule> Schedules { get; }
        List<Invoice> Invoices { get; }
        List<Ticket> Tickets { get; }
    }

    public interface IBookingService
    {
        void CreateUser(string name, long mobile, string email);

        List<User> GetAllUsers();

        void CreateBus(string coachNumber, BusClassification classification);

        List<Bus> GetAllBuses();

        void CreateSchedule(int busId, string departureCity, string arrivalCity, DateTime departureTime, decimal price);

        List<Schedule> GetAllSchedules();

        Schedule GetScheduleById(int scheduleId);

        bool BookTicket(int userId, int scheduleId, string seatNumber, out string message);

        List<Ticket> GetUserTickets(int userId);
    }

    public interface IBillingService
    {
        List<Invoice> GetUserInvoices(int userId);

        bool ProcessPayment(int invoiceId, out string message);
    }


    public enum BusClassification
    {
        Business,
        Economy
    }

    public enum PaymentStatus
    {
        Unpaid,
        Paid
    }

    public class User
    {
        public int UserId { get; }
        public string FullName { get; set; }
        public long MobileNumber { get; set; }
        public string Email { get; set; }

        public User(int userId, string fullName, long mobileNumber, string email)
        {
            UserId = userId;
            FullName = fullName;
            MobileNumber = mobileNumber;
            Email = email;
        }
    }

   
    public abstract class Bus
    {
        public int BusId { get; }

        public string CoachNumber { get; }

        public BusClassification Classification { get; }

        public int SeatingCapacity { get; protected set; }

        public List<string> SeatLayout { get; protected set; } = new List<string>();


        protected Bus(int busId, string coachNumber, BusClassification classification)
        {
            BusId = busId;
            CoachNumber = coachNumber;
            Classification = classification;
        }

        public abstract bool IsValidSeat(string seatNumber);
    }

    
    public class BusinessBus : Bus
    {
        public BusinessBus(int busId, string coachNumber) : base(busId, coachNumber, BusClassification.Business)
        {
            SeatingCapacity = 24;
            GenerateSeats();
        }

        private void GenerateSeats()
        {
           
            for (int i = 1; i <= 8; i++)
            {
                SeatLayout.Add($"A{i}");
                SeatLayout.Add($"B{i}");
                SeatLayout.Add($"C{i}");
            }
        }

        public override bool IsValidSeat(string seatNumber) => SeatLayout.Contains(seatNumber.ToUpper());
    }

    public class EconomyBus : Bus
    {
        public EconomyBus(int busId, string coachNumber) : base(busId, coachNumber, BusClassification.Economy)
        {
            SeatingCapacity = 40;
            GenerateSeats();
        }

        private void GenerateSeats()
        {
           
            for (int i = 1; i <= 10; i++)
            {
                SeatLayout.Add($"A{i}");
                SeatLayout.Add($"B{i}");
                SeatLayout.Add($"C{i}");
                SeatLayout.Add($"D{i}");
            }
        }

        public override bool IsValidSeat(string seatNumber) => SeatLayout.Contains(seatNumber.ToUpper());
    }

    public class Schedule
    {
        public int ScheduleId { get; }
        public Bus AssignedBus { get; }
        public string DepartureCity { get; }
        public string ArrivalCity { get; }
        public DateTime DepartureTime { get; }
        public decimal TicketPrice { get; }
        
       
        public HashSet<string> ReservedSeats { get; } = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        public Schedule(int scheduleId, Bus bus, string departureCity, string arrivalCity, DateTime departureTime, decimal ticketPrice)
        {
            ScheduleId = scheduleId;
            AssignedBus = bus;
            DepartureCity = departureCity;
            ArrivalCity = arrivalCity;
            DepartureTime = departureTime;
            TicketPrice = ticketPrice;
        }
    }

    public class Ticket
    {
        public int TicketId { get; }
        public int UserId { get; }
        public int ScheduleId { get; }
        public string SeatNumber { get; }

        public Ticket(int ticketId, int userId, int scheduleId, string seatNumber)
        {
            TicketId = ticketId;
            UserId = userId;
            ScheduleId = scheduleId;
            SeatNumber = seatNumber;
        }
    }

    public class Invoice
    {
        public int InvoiceId { get; }
        public int TicketId { get; }
        public int UserId { get; }
        public decimal AmountDue { get; }
        public DateTime GenerationDate { get; }
        public PaymentStatus Status { get; set; }

        public Invoice(int invoiceId, int ticketId, int userId, decimal amountDue)
        {
            InvoiceId = invoiceId;
            TicketId = ticketId;
            UserId = userId;
            AmountDue = amountDue;
            GenerationDate = DateTime.Now;
            Status = PaymentStatus.Unpaid;
        }
    }

    

    public class InMemoryDataStore : IDataStore
    {
        private static InMemoryDataStore _instance;
        public static InMemoryDataStore Instance => _instance ??= new InMemoryDataStore();

        public List<User> Users { get; } = new List<User>();
        public List<Bus> Buses { get; } = new List<Bus>();
        public List<Schedule> Schedules { get; } = new List<Schedule>();
        public List<Invoice> Invoices { get; } = new List<Invoice>();
        public List<Ticket> Tickets { get; } = new List<Ticket>();

        private InMemoryDataStore() 
        {
            
            Users.Add(new User(101, "Nayan Chandra", 1640378034, "nayan@gmail.com"));
            Buses.Add(new BusinessBus(1, "CH-9921"));
            Buses.Add(new EconomyBus(2, "DH-4412"));
            Schedules.Add(new Schedule(501, Buses[0], "Dhaka", "Chittagong", DateTime.Now.AddDays(1).AddHours(8), 1200.00m));
            Schedules.Add(new Schedule(502, Buses[1], "Dhaka", "Sylhet", DateTime.Now.AddDays(2).AddHours(22), 750.00m));
        }
    }



    public class BookingService : IBookingService
    {
        private readonly IDataStore _db;

        public BookingService(IDataStore db) => _db = db;

        public void CreateUser(string name, long mobile, string email)
        {
            int nextId = _db.Users.Count > 0 ? _db.Users.Max(u => u.UserId) + 1 : 101;
            _db.Users.Add(new User(nextId, name, mobile, email));
        }

        public List<User> GetAllUsers() => _db.Users;

        public void CreateBus(string coachNumber, BusClassification classification)
        {
            int nextId = _db.Buses.Count > 0 ? _db.Buses.Max(b => b.BusId) + 1 : 1;
            Bus newBus = classification == BusClassification.Business 
                ? new BusinessBus(nextId, coachNumber) 
                : new EconomyBus(nextId, coachNumber);
            _db.Buses.Add(newBus);
        }

        public List<Bus> GetAllBuses() => _db.Buses;


        public void CreateSchedule(int busId, string departureCity, string arrivalCity, DateTime departureTime, decimal price)
        {
            Bus bus = _db.Buses.FirstOrDefault(b => b.BusId == busId);
            if (bus == null) throw new ArgumentException("Bus ID not found.");

            int nextId = _db.Schedules.Count > 0 ? _db.Schedules.Max(s => s.ScheduleId) + 1 : 501;
            _db.Schedules.Add(new Schedule(nextId, bus, departureCity, arrivalCity, departureTime, price));
        }

        public List<Schedule> GetAllSchedules() => _db.Schedules;

        public Schedule GetScheduleById(int scheduleId) => _db.Schedules.FirstOrDefault(s => s.ScheduleId == scheduleId);

        public bool BookTicket(int userId, int scheduleId, string seatNumber, out string message)
        {
            User user = _db.Users.FirstOrDefault(u => u.UserId == userId);
            Schedule schedule = _db.Schedules.FirstOrDefault(s => s.ScheduleId == scheduleId);

            if (user == null) { message = "Error: Invalid User ID."; return false; }
            if (schedule == null) { message = "Error: Invalid Schedule ID."; return false; }
            
          
            if (!schedule.AssignedBus.IsValidSeat(seatNumber))
            {
                message = $"Error: Seat {seatNumber} is invalid for this configuration layout.";
                return false;
            }

            
            if (schedule.ReservedSeats.Contains(seatNumber))
            {
                message = $"Error: Seat {seatNumber} is already reserved.";
                return false;
            }

            
            int nextTicketId = _db.Tickets.Count > 0 ? _db.Tickets.Max(t => t.TicketId) + 1001 : 1001;
            Ticket ticket = new Ticket(nextTicketId, userId, scheduleId, seatNumber);
            
            int nextInvoiceId = _db.Invoices.Count > 0 ? _db.Invoices.Max(i => i.InvoiceId) + 9001 : 9001;
            Invoice invoice = new Invoice(nextInvoiceId, nextTicketId, userId, schedule.TicketPrice);

            _db.Tickets.Add(ticket);
            _db.Invoices.Add(invoice);
            
            
            schedule.ReservedSeats.Add(seatNumber);

            message = $"Success! Ticket reserved. Ticket ID: {ticket.TicketId}. Invoice ID: {invoice.InvoiceId}. Please make payment.";
            return true;
        }

        public List<Ticket> GetUserTickets(int userId) => _db.Tickets.Where(t => t.UserId == userId).ToList();
    }

    public class BillingService : IBillingService
    {
        private readonly IDataStore _db;

        public BillingService(IDataStore db) => _db = db;

        public List<Invoice> GetUserInvoices(int userId) => _db.Invoices.Where(i => i.UserId == userId).ToList();

        public bool ProcessPayment(int invoiceId, out string message)
        {
            Invoice invoice = _db.Invoices.FirstOrDefault(i => i.InvoiceId == invoiceId);
            if (invoice == null) { message = "Error: Invoice ID not found."; return false; }

            if (invoice.Status == PaymentStatus.Paid)
            {
                message = "Notice: This invoice is already paid.";

                return false;
            }

            invoice.Status = PaymentStatus.Paid;

            message = $"Success! Payment of ${invoice.AmountDue} confirmed for Invoice ID: {invoiceId}.";

            return true;
        }
    }


    class Program
    {
        private static IBookingService _bookingService;
        private static IBillingService _billingService;

        static void Main()
        {
            
            IDataStore db = InMemoryDataStore.Instance;
            _bookingService = new BookingService(db);
            _billingService = new BillingService(db);

            RunMenu();
        }

        private static void RunMenu()
        {
            while (true)
            {
                Console.WriteLine("   BUS TICKET BOOKING & BILLING CONSOLE SYSTEM    ");
                Console.WriteLine("==================================================");
                Console.WriteLine("1. Create User");
                Console.WriteLine("2. Display All Users");
                Console.WriteLine("3. Create Bus");
                Console.WriteLine("4. Display All Buses");
                Console.WriteLine("5. Create Schedule");
                Console.WriteLine("6. Display All Schedules");
                Console.WriteLine("7. Display Schedule Details (Seat Map)");
                Console.WriteLine("8. Book Ticket");
                Console.WriteLine("9. Display User Invoices");
                Console.WriteLine("10. Process Invoice Payment");
                Console.WriteLine("11. Display User Tickets");
                Console.WriteLine("0. Exit Application");
                Console.WriteLine("==================================================");
                Console.Write("Enter option choice selection: ");

                string input = Console.ReadLine();
                if (input == "0") break;

                ProcessCommand(input);
                Console.WriteLine("\nPress any key to return to menu...");
                Console.ReadKey();
            }
        }

        private static void ProcessCommand(string choice)
        {
            Console.Clear();
            try
            {
                switch (choice)
                {
                    case "1":
                        Console.WriteLine("--- Create New User ---");

                        Console.Write("Enter Full Name: "); string name = Console.ReadLine();

                        Console.Write("Enter Mobile Number: "); long mobile = long.Parse(Console.ReadLine());

                        Console.Write("Enter Email Address: "); string email = Console.ReadLine();
                        _bookingService.CreateUser(name, mobile, email);

                        Console.WriteLine("User created successfully!");
                        break;

                    case "2":
                        Console.WriteLine("--- All System Users ---");

                        foreach (var u in _bookingService.GetAllUsers())

                            Console.WriteLine($"ID: {u.UserId} | Name: {u.FullName} | Ph: {u.MobileNumber} | Email: {u.Email}");
                        break;

                    case "3":
                        Console.WriteLine("--- Create New Bus Asset ---");

                        Console.Write("Enter Coach Reg Number (e.g. DH-5521): "); string coach = Console.ReadLine();

                        Console.Write("Enter Classification Type (1 for Business, 2 for Economy): ");
                        string typeChoice = Console.ReadLine();

                        BusClassification cls = typeChoice == "1" ? BusClassification.Business : BusClassification.Economy;

                        _bookingService.CreateBus(coach, cls);

                        Console.WriteLine("Bus successfully written to system fleet inventory!");
                        break;

                    case "4":

                        Console.WriteLine("--- Fleet Bus Inventory ---");
                        
                        foreach (var b in _bookingService.GetAllBuses())
                            Console.WriteLine($"Bus ID: {b.BusId} | Coach: {b.CoachNumber} | Class: {b.Classification} | Total Seats: {b.SeatingCapacity}");
                        break;

                    case "5":
                        Console.WriteLine("--- Create Fleet Route Run Schedule ---");

                        Console.Write("Enter Bus ID to assign: "); int bId = int.Parse(Console.ReadLine());

                        Console.Write("Departure City: "); string dep = Console.ReadLine();

                        Console.Write("Arrival Destination City: "); string arr = Console.ReadLine();
                        Console.Write("Departure Time (YYYY-MM-DD HH:MM): "); DateTime dTime = DateTime.Parse(Console.ReadLine());

                        Console.Write("Base Ticket Face Value Price: "); decimal prc = decimal.Parse(Console.ReadLine());

                        _bookingService.CreateSchedule(bId, dep, arr, dTime, prc);

                        Console.WriteLine("Schedule initialized successfully!");
                        break;

                    case "6":
                        Console.WriteLine("--- Available Active Operating Schedules ---");
                        foreach (var s in _bookingService.GetAllSchedules())

                            Console.WriteLine($"Sched ID: {s.ScheduleId} | Route: {s.DepartureCity} to {s.ArrivalCity} | Time: {s.DepartureTime} | Price: ${s.TicketPrice} [Coach: {s.AssignedBus.CoachNumber}]");
                        break;

                    case "7":
                        Console.WriteLine("--- Schedule Seat Map Availability View ---");
                        Console.Write("Enter target Schedule ID: "); int sIdMap = int.Parse(Console.ReadLine());
                        Schedule sched = _bookingService.GetScheduleById(sIdMap);
                        if (sched != null)
                        {
                            Console.WriteLine($"\nRoute: {sched.DepartureCity} -> {sched.ArrivalCity} ({sched.AssignedBus.Classification} Class)");

                            Console.WriteLine("--------------------------------------------------");
                            Console.WriteLine("Seat layout status below ([XX] = Sold/Reserved):");
                            int count = 0;
                            foreach (var seat in sched.AssignedBus.SeatLayout)
                            {
                                string display = sched.ReservedSeats.Contains(seat) ? "[XX]" : $"[{seat}]";
                                Console.Write($"{display,-7}");
                                count++;
                                if (count % 4 == 0) Console.WriteLine();
                            }
                            Console.WriteLine();
                        }
                        else Console.WriteLine("System execution issue: Schedule match lookup returned empty target reference.");
                        break;

                    case "8":
                        Console.WriteLine("--- Real-time Ticket Reservation Booking Processing ---");
                        Console.Write("Enter existing User ID: "); int uId = int.Parse(Console.ReadLine());
                        Console.Write("Enter Active Schedule ID: "); int sId = int.Parse(Console.ReadLine());
                        Console.Write("Enter requested Seat Designator identifier (e.g. A1): "); string seatNum = Console.ReadLine();

                        if (_bookingService.BookTicket(uId, sId, seatNum, out string resMsg))
                            Console.ForegroundColor = ConsoleColor.Green;
                        else
                            Console.ForegroundColor = ConsoleColor.Yellow;

                        Console.WriteLine($"\n{resMsg}");
                        Console.ResetColor();
                        break;

                    case "9":
                        Console.WriteLine("--- Ledger User Financial Invoices View ---");
                        Console.Write("Enter searching User ID: "); int invUId = int.Parse(Console.ReadLine());
                        var invoices = _billingService.GetUserInvoices(invUId);
                        foreach (var inv in invoices)
                            Console.WriteLine($"Invoice Num: {inv.InvoiceId} | Attached Ticket Ref: {inv.TicketId} | Due: ${inv.AmountDue} | Date: {inv.GenerationDate.ToShortDateString()} | Status: [{inv.Status}]");
                        break;

                    case "10":
                        Console.WriteLine("--- Secure Payment Reconciliation Module ---");
                        Console.Write("Enter exact open Invoice ID reference: "); int payInvId = int.Parse(Console.ReadLine());
                        if (_billingService.ProcessPayment(payInvId, out string payMsg))
                            Console.ForegroundColor = ConsoleColor.Green;
                        else
                            Console.ForegroundColor = ConsoleColor.Yellow;

                        Console.WriteLine($"\n{payMsg}");
                        Console.ResetColor();
                        break;

                    case "11":
                        Console.WriteLine("--- System Issued Active Passenger Tickets ---");
                        Console.Write("Enter Passenger User ID value: "); int tUId = int.Parse(Console.ReadLine());
                        var tickets = _bookingService.GetUserTickets(tUId);
                        foreach (var t in tickets)
                        {
                            var s = _bookingService.GetScheduleById(t.ScheduleId);
                            Console.WriteLine($"Ticket Ref ID: {t.TicketId} | Route: {s.DepartureCity} to {s.ArrivalCity} | Seat Assigned: {t.SeatNumber} | Departure: {s.DepartureTime}");
                        }
                        break;

                    default:
                        Console.WriteLine("Command recognition exception. Please choose options bounded on the layout schema menu list.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Operational error parsing parameters: {ex.Message}");
                Console.ResetColor();
            }
        }
    }

    
}