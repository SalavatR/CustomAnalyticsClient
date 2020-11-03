using System;
using CustomGameAnalytics.Scripts.Common;
using CustomGameAnalytics.Scripts.Events;
using CustomGameAnalytics.Scripts.Events.Data;
using CustomGameAnalytics.Scripts.Settings;
using UnityEngine;

namespace CustomGameAnalytics.Scripts
{
    [RequireComponent(typeof(CoroutineManager))]
    public class EventsManager : MonoBehaviour
    {
        public static EventsManager Instance;

        public AnalyticsSettings settings;
        private EventsSender _eventsSender;

        private void Awake()
        {
            if (!Instance) Instance = this;
            else
            {
                Debug.LogError("Only one instance can be on scene");
                Destroy(this);
            }

            _eventsSender = new EventsSender(settings.serverAddress, settings.cooldownBeforeSend);
        }

        public void TrackEvent(EventData eventData)
        {
            eventData.time = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            eventData.version = Application.version;
            var ev = new CustomEvent {type = eventData.type, data = eventData};

            _eventsSender.AddEvent(ev);
        }
    }
}