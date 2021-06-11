using Abode.Core;
using Abode.Domain.Models;
using Azure.DigitalTwins.Core;
using System.Threading;
using System.Threading.Tasks;

namespace Abode.Domain
{
    public class GetThermostatHandler : IHandleQuery<GetThermostat, Thermostat>
    {
        private readonly DigitalTwinsClient _twins;

        public GetThermostatHandler(DigitalTwinsClient twins)
        {
            _twins = twins;
        }

        public async Task<Thermostat> Execute(GetThermostat query, CancellationToken cancellationToken)
        {
            var response = await _twins.GetDigitalTwinAsync<Thermostat>(query.Id, cancellationToken);
            return response.Value;
        }
    }
}
