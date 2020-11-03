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
        private static EventsManager Instance;

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

        private void TrackEventLocal(EventData eventData)
        {
            if (!settings)
            {
                Debug.LogError("Please Add Settings File");
            }

            eventData.time = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            eventData.version = Application.version;
            var ev = new CustomEvent {type = eventData.type, data = eventData};

            _eventsSender.AddEvent(ev);
        }

        public static void TrackEvent(EventData eventData)
        {
            if (!Instance)
            {
                Debug.LogError("Please add EventsManager component to the scene");
            }

            Instance.TrackEventLocal(eventData);
        }
    }
}