using System;
using System.Collections.Generic;
using UnityEngine;

namespace CustomGameAnalytics.Scripts.Events
{
    [Serializable]
    internal class EventPackage
    {
        [SerializeField]
        public List<CustomEvent> events;
    }
}