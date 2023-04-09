using Grpc.Net.Client;
using Newtonsoft.Json;
using ProductGrpcService;

using var channel = GrpcChannel.ForAddress("https://localhost:5001");
var client = new Product.ProductClient(channel);

Console.WriteLine("Enter product name:");
string name = Console.ReadLine();
Console.WriteLine("Enter product price:");
double price = Convert.ToDouble(Console.ReadLine());

var request = new ProductRequest()
{
    Name = name,
    Price = price
};
var reply1 = await client.CreateAsync(request);
var reply2 = await client.ListAsync(new Empty());

Console.WriteLine(reply2.Products.Count);
