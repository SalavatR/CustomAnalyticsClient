using System.Collections.Generic;
using System.Linq;
using CustomGameAnalytics.Scripts.Common;
using CustomGameAnalytics.Scripts.Events;
using CustomGameAnalytics.Scripts.Settings;

namespace CustomGameAnalytics.Scripts
{
    internal class StorageEvents
    {
        private readonly List<CustomEvent> _events = new List<CustomEvent>();
        ILocalInfoStorage<List<CustomEvent>> _storage = new LocalInfoStorage<List<CustomEvent>>(AnalyticsConstants.EventsFileName);

        public StorageEvents()
        {
            var evnts = _storage.Load();
            if (evnts != null) _events = _storage.Load();
        }

        public void StoreEvent(CustomEvent eventCustom)
        {
            _events.Add(eventCustom);
            _storage.Save(_events);
        }

        public void StoreEvent(List<CustomEvent> eventBase)
        {
            _events.AddRange(eventBase);
            _storage.Save(_events);
        }

        public List<CustomEvent> GetEvents()
        {
            var result = _events.ToList();
            _events.Clear();
            _storage.Save(_events);
            return result;
        }
    }
}