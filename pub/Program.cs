using System;
using Dapr.Client;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Collections.Generic;

for (int i = 1; i <= 5; i++)
{
    var order = new Order(i);
    using var client = new DaprClientBuilder().Build();
    Dictionary<string, string> valuePairs = new Dictionary<string, string>();
    valuePairs.Add("rawPayload", "true");
    // Publish an event/message using Dapr PubSub
    await client.PublishEventAsync("orderpubsub", "orders", order
    , 
    new Dictionary<string, string>
    {
        ["rawPayload"] = "true"
    }
    );
    Console.WriteLine("Published data: " + order);

    await Task.Delay(TimeSpan.FromSeconds(1));
}

public record Order([property: JsonPropertyName("orderId")] int OrderId);
