using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;
using Grpc.Net.Client.Web;
using Grpc.Net.Client;
using BlazorBarcode.gRPC;
using BlazorBarcode.Client.State;

namespace BlazorBarcode.Client
{
    public class Program
    {
        public const string BackendUrl = "https://localhost:5001";
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddScoped(services =>
            {
                // Create a GRPC Web channel
                var httpClient = new HttpClient(new GrpcWebHandler(GrpcWebMode.GrpcWeb, new HttpClientHandler()));
                var channel = GrpcChannel.ForAddress(new Uri(BackendUrl), new GrpcChannelOptions { HttpClient = httpClient });

                // Instantiating GRPC clients for this channel
                return new Inventory.InventoryClient(channel);
            });

            builder.Services.AddScoped<ProductsState>();

            builder.Services.AddBaseAddressHttpClient();

            await builder.Build().RunAsync();
        }
    }
}
