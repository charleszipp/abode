using Abode.Core;
using Abode.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Routing;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Abode.Api
{
    public class ThermostatController
    {
        private readonly Mediator _mediator;

        public ThermostatController(Mediator mediator) => 
            _mediator = mediator;

        [FunctionName("thermostats_add")]
        public async Task<IActionResult> AddThermostat(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "thermostats/{id}")] AddThermostat command,
            ILogger log,
            CancellationToken token)
        {            
            await _mediator.ExecuteAsync(command, token);
            return new CreatedAtRouteResult("thermostats_get", new { id = command.Id });
        }

        [FunctionName("thermostats_get")]
        public async Task<IActionResult> GetThermostat(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "thermostats/{id}")] HttpRequest req,
            string id,
            ILogger log,
            CancellationToken token)
        {
            var thermostat = await _mediator.Query(new GetThermostat(id), token);
            return new OkObjectResult(thermostat);
        }
    }
}
