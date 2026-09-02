// var emailService = new EmailService();
// var notificationService = new NotificationService(emailService);
// that is not good because we are creating the dependency in the high level module. so we will use dependency injection to inject the dependency from outside.

//var notificationService = ObjectFactory<NotificationService>.Get(Key:"notification-Service"); // onno khan theke object create kore reurn kortechi 
// var notificationService = ObjectFactory<NotificationService>.Get(); // onno khan theke object create kore reurn kortechi // / this is the high level module. we are getting the dependency from outside using object factory. so we are not creating the dependency in the high level module. so we are following the DIP principle.
// notificationService.NotifyUser("hasan@gmail.com","Hello Hasan");

// dotnet build in labary
// using Microsoft.Extensions.DependencyInjection;

// dependnecy injection er 2 ta life cycle ache. 1. register 2. resolver
// regiter ------------------------------------------------------------------------
//var service=new ServiceCollection();
var service=new CustomServiceCollection(); // custom service collection created

service.AddTransient<NotificationService,NotificationService>();
service.AddTransient<IEmailService,EmailService>(); /// like dictionary / key value pair

// life time
service.AddTransient<ITransientService,TransientService>(); // every time new instance will be created
service.AddScoped<IScopedService,ScopedService>(); // every time same instance will be created in the same scope but different instance will be created in different scope
 service.AddSingleton<ISingletonService,SingletonService>(); // every time single instance will be created during application life time

// resolver--------------------------------------------------------------------------------
var serviceProvider=service.BuildServiceProvider(); // given by library

var notificationService=serviceProvider.GetRequiredService<NotificationService>();
notificationService.NotifyUser("hasan@gmail.com","Hello Hasan");

// life time
// transient service
var transientService1=serviceProvider.GetRequiredService<ITransientService>(); // for one service
//var transientService1=serviceProvider.GetRequiredService<IEnumerable<ITransientService>>(); // for multiple service

//var transientService1=new TransientService();                     
Console.WriteLine($"Transient Service 1: {transientService1.Id}");
var transientService2=serviceProvider.GetRequiredService<ITransientService>();
Console.WriteLine($"Transient Service 2: {transientService2.Id}");
Console.WriteLine();

// singleton service
var singletonService1=serviceProvider.GetRequiredService<ISingletonService>();
Console.WriteLine($"Singleton Service 1: {singletonService1.Id}");  
var singletonService2=serviceProvider.GetRequiredService<ISingletonService>();
Console.WriteLine($"Singleton Service 2: {singletonService2.Id}");
Console.WriteLine();

// scoped service
using (var scope = serviceProvider.CreateScope()) // create scope
{
    var scopedService1=scope.ServiceProvider.GetRequiredService<IScopedService>();
    Console.WriteLine($"Scoped Service 1: {scopedService1.Id}");
    var scopedService2=scope.ServiceProvider.GetRequiredService<IScopedService>();  
    Console.WriteLine($"Scoped Service 2: {scopedService2.Id}");
}

using (var scope = serviceProvider.CreateScope()) // create 2nd scope
{
    var scopedService3=scope.ServiceProvider.GetRequiredService<IScopedService>();
    Console.WriteLine($"Scoped Service 3: {scopedService3.Id}");
    var scopedService4=scope.ServiceProvider.GetRequiredService<IScopedService>();  
    Console.WriteLine($"Scoped Service 4: {scopedService4.Id}");
}