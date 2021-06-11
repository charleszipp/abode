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
        private readonly CancellationTokenSource _tokenSource;

        public TwinEventsHandler(Mediator mediator, CancellationTokenSource tokenSource)
        {
            _mediator = mediator;
            _tokenSource = tokenSource;
        }


        //[FunctionName("twin_changed")]
        //public void TwinChangedHandler([EventGridTrigger] EventGridEvent e, ILogger log)
        //{
        //    var twinChangedEvent = ((JObject)e.Data).SelectToken("$.data").ToObject<TwinChanged>();

        //    // Cloud event doesnt appear to be compatible since ADT event property names contain characters that are not valid with CloudEvent format such as
        //    // '$'. Deserializing generated error indicating the message contains non-valid ASCII characters. Expects a-z and/or 0-9 only.
        //    //var twinChangedEvent = e.Data.ToObjectFromJson<TwinChanged>();

        //    log.LogTrace("twin_changed received event", e);
        //}

        [FunctionName("twin_changed")]
        public void TwihChangedHandler([EventHubTrigger("ehb-twins-abd", Connection = "event_hubs_ns_connection_string")] TwinChanged e, ILogger log) 
        {
            //string data = Encoding.UTF8.GetString(e.Body);
            log.LogInformation("twin_changed received event", e);
        }
    }
}
