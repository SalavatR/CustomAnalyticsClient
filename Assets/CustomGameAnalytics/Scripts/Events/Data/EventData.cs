using System;
using Newtonsoft.Json;
using UnityEngine;

namespace CustomGameAnalytics.Scripts.Events.Data
{
    [Serializable]
    public abstract class EventData 
    {
        [JsonIgnore] internal abstract string type { get; }
        [JsonProperty] internal long time;
        [JsonProperty] internal string version;
    }
}