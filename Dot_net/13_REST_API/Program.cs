using System.IO.Pipelines;
using System.Runtime.Intrinsics.Arm;

// Dependecy Injection 
var builder=WebApplication.CreateBuilder(args);
builder.Servoces.AppOpenApi();
var app:WebApplication=builder.Builder();

// MiddleWare configuration
if(app.Environment.IsDevelopment())app.MapOpenApi();
app.UseHttpsRedirection();

// End point configuration
app.MapPost(Pattern:"/Product",(HttpContent context,[FromBody] Product product)=>{
    Console.WriteLine($"Received name {product.Name} and price {product.Price}");
    return Results.OK(new {Message="product added successfully"});
});

app.MapGet(Patten:"/Weatherforcast",(string message)=>{
     Console.WriteLine($"Recevied message: {message}");
    return Results.OK(new{Message=message});
}).WithName("GetWeatherforcast ");
app.run();

public class Product
{
    public string Name{get; set;}
    public decimal Price{get; set;}
}