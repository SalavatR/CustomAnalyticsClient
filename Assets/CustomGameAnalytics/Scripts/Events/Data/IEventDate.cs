using UnityEngine;

namespace CustomGameAnalytics.Scripts.Events.Data
{
    public interface IEventDate
    {
        string type { get; }

        long time { get; set; }

        string version { get; set; }
    }
}