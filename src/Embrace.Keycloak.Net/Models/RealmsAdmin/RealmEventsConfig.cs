﻿using Newtonsoft.Json;
using System.Collections.Generic;

namespace Keycloak.Net.Models.RealmsAdmin
{
    public class RealmEventsConfig
    {
        [JsonProperty("adminEventsDetailsEnabled")]
        public bool? AdminEventsDetailsEnabled { get; set; }
        [JsonProperty("adminEventsEnabled")]
        public bool? AdminEventsEnabled { get; set; }
        [JsonProperty("enabledEventTypes")]
        public IEnumerable<string> EnabledEventTypes { get; set; }
        [JsonProperty("eventsEnabled")]
        public bool? EventsEnabled { get; set; }
        [JsonProperty("eventsExpiration")]
        public long? EventsExpiration { get; set; }
        [JsonProperty("eventsListeners")]
        public IEnumerable<string> EventsListeners { get; set; }
    }
}
