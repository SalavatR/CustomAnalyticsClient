using System;
using CustomGameAnalytics.Scripts.Settings;

namespace CustomGameAnalytics.Scripts.Events.Data
{
    
    [Serializable]
    public class PaymentData : EventData
    {
        public string paymentId;
        public float price;
        internal override string type => EventTypes.Payment;
    }
}