using Abode.Core;
using Abode.Domain.Models;
using Azure;
using Azure.DigitalTwins.Core;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Abode.Domain
{
    public class AddThermostat : ICommand
    {
        public AddThermostat(string id, string name)
        {
            Id = id;
            Name = name;
        }

        public string Id { get; }
        public string Name { get; }
    }

    public class AddThermostatHandler : IHandleCommand<AddThermostat>
    {
        private readonly DigitalTwinsClient _twins;

        public AddThermostatHandler(DigitalTwinsClient twins)
        {
            _twins = twins;
        }

        public async Task Execute(AddThermostat command, CancellationToken cancellationToken)
        {
            var thermostat = new Thermostat
            {
                Id = command.Id,
                Name = command.Name
            };

            try
            {
                await _twins.CreateOrReplaceDigitalTwinAsync(command.Id, thermostat, cancellationToken: cancellationToken);
            }
            catch (RequestFailedException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
