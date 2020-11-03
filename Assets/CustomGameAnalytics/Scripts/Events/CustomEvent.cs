using System;
using System.Xml.Serialization;
using CustomGameAnalytics.Scripts.Events.Data;
using UnityEngine;

namespace CustomGameAnalytics.Scripts.Events
{
    [Serializable]
    internal class CustomEvent
    {
        public string type;

        public EventData data;
    }
}