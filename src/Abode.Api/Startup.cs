using Abode.Core;
using Abode.Domain;
using Abode.Domain.Models;
using Azure.DigitalTwins.Core;
using Azure.Identity;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Threading;

[assembly: FunctionsStartup(typeof(Abode.Api.Startup))]

namespace Abode.Api
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var environment = Environment.GetEnvironmentVariable("ENVIRONMENT") ?? "local";

            var configBuilder = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile($"{environment}.settings.json", optional: true, reloadOnChange: false)
               .AddEnvironmentVariables();

            var config = configBuilder.Build();

            builder.Services
                .AddScoped<Mediator>(sp => new Mediator(sp))
                .AddScoped<IHandleCommand<AddThermostat>, AddThermostatHandler>()
                .AddScoped<IHandleQuery<GetThermostat, Thermostat>, GetThermostatHandler>()
                .AddScoped<DigitalTwinsClient>(sp =>
                    new DigitalTwinsClient(new Uri(config["adt_instance_url"]), new DefaultAzureCredential())
                );
        }
    }
}
