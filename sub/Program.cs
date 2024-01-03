using System.Text;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using Dapr;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

// Dapr will send serialized event object vs. being raw CloudEvent
//app.UseCloudEvents();

// needed for Dapr pub/sub routing
app.MapSubscribeHandler();

if (app.Environment.IsDevelopment()) { app.UseDeveloperExceptionPage(); }

// Dapr subscription in [Topic] routes orders topic to this route

app.MapPost("/orders", [Topic("orderpubsub", "orders", enableRawPayload: true)] ([FromBody]JsonObject order) =>
{
   Console.WriteLine("Subscriber received : " + order);
   var base64 = order["data_base64"]?.GetValue<string>();
   var theMessage = Encoding.UTF8.GetString(Convert.FromBase64String(base64));//Convert
   return Results.Ok(order);
});


await app.RunAsync();

public record Order([property: JsonPropertyName("orderId")] int OrderId);
