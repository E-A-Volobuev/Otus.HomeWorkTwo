using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;
using WebClient.Mapping;
using WebClient.Services;
using WebClient.Services.Abstraction;

namespace WebClient
{
    static class Program
    {
        static async Task Main(string[] args)
        {
            HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);
            builder.Services.AddHostedService<Start>();
            builder.Services.AddScoped<ICustomerService, CustomerService>();
            builder.Services.AddScoped<IHttpService, HttpService>();
            builder.Services.AddAutoMapper(typeof(CustomerMappingProfile));

            using IHost host = builder.Build();
            await host.RunAsync();
        }       
    }
}