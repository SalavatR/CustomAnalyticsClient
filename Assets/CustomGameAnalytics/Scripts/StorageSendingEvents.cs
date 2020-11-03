using System;
using System.Collections.Generic;
using System.Linq;
using CustomGameAnalytics.Scripts.Common;
using CustomGameAnalytics.Scripts.Events;
using CustomGameAnalytics.Scripts.Settings;

namespace CustomGameAnalytics.Scripts
{
    internal class StorageSendingEvents
    {
        private Dictionary<string, List<CustomEvent>> _events = new Dictionary<string, List<CustomEvent>>();

        private readonly ILocalInfoStorage<List<CustomEvent>> _storage =
            new LocalInfoStorage<List<CustomEvent>>(AnalyticsConstants.SendingEventsFileName);

        private void Save()
        {
            _storage.Save(GetAllEvents());
        }

        public string StoreEvent(List<CustomEvent> eventBase)
        {
            var id = Guid.NewGuid().ToString();
            _events.Add(id, eventBase);
            Save();
            return id;
        }

        public void RemoveEvent(string id)
        {
            _events.Remove(id);
            Save();
        }

        public void RemoveAll()
        {
            _events.Clear();
            Save();
        }

        public List<CustomEvent> GetEvents(string id)
        {
            _events.TryGetValue(id, out var events);
            return events;
        }

        public List<CustomEvent> GetAllEvents()
        {
            return _events.Values.SelectMany(x => x).ToList();
        }

        public List<CustomEvent> GetFromDisk()
        {
            var loadedEvents = _storage.Load();
            return loadedEvents;
        }
    }
}