using Abode.Core;
using Abode.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Abode.Domain
{
    public class GetThermostat : IQuery<Thermostat>
    {
        public GetThermostat(string id)
        {
            Id = id;
        }

        public string Id { get; }
    }
}
