// See https://aka.ms/new-console-template for more information

using System;
using System.Linq;
using System.Threading;
using Grpc.Core;
using Grpc.Net.Client;
using OzonEdu.StockApi.Grpc;

using var channel = GrpcChannel.ForAddress("https://localhost:5001");
var client = new StockApiGrpc.StockApiGrpcClient(channel);

// var response = await client.GetAllStockItemsAsync(new GetAllStockItemsRequest(), cancellationToken: CancellationToken.None);
// foreach (var item in response.Stocks)
// {
//     Console.WriteLine($"item id {item.ItemId} - quantity {item.Quantity}");
// }

try
{
    await client.AddStockItemAsync(new AddStockItemRequest { Quantity = 1, ItemName = "item to add"});
}
catch (RpcException e)
{
    Console.WriteLine(e);
}

// var streamingCall = client.GetAllStockItemsStreaming(new GetAllStockItemsRequest());
// await foreach (var item in streamingCall.ResponseStream.ReadAllAsync())
// {
//     Console.WriteLine($"item id {item.ItemId} - quantity {item.Quantity}");
// }


// while (await streamingCall.ResponseStream.MoveNext())
// {
//     var item = streamingCall.ResponseStream.Current;
//     Console.WriteLine($"item id {item.ItemId} - quantity {item.Quantity}");
// }

var clientStreamingCall = client.AddStockItemStreaming(cancellationToken: CancellationToken.None);
await clientStreamingCall.RequestStream.WriteAsync(new AddStockItemRequest
{
    Quantity = 23,
    ItemName = "Shoes"
});
await clientStreamingCall.RequestStream.WriteAsync(new AddStockItemRequest
{
    Quantity = 78,
    ItemName = "Cap"
});
await clientStreamingCall.RequestStream.CompleteAsync();
Console.ReadKey();