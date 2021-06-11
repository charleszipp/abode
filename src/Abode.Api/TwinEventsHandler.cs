using Abode.Core;
using Abode.Domain;
using Azure.Messaging;
using Microsoft.Azure.EventGrid.Models;
using Microsoft.Azure.EventHubs;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.EventGrid;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading;

namespace Abode.Api
{
    public class TwinEventsHandler
    {
        private readonly Mediator _mediator;

        public TwinEventsHandler(Mediator mediator) => 
            _mediator = mediator;

        [FunctionName("twin_events_handler")]
        public void Handle([EventHubTrigger("evh-twins", Connection = "evh_twins_connection_string")] EventData e, 
            ILogger log, 
            CancellationToken cancellationToken) 
        {
            log.LogInformation("twin event received", e);
        }
    }
}
