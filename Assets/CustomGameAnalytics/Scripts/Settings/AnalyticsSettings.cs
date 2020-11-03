using UnityEngine;

namespace CustomGameAnalytics.Scripts.Settings
{
    [CreateAssetMenu(fileName = "AnalyticsSettings", menuName = "ScriptableObjects/AnalyticsSettings", order = 1)]
    public class AnalyticsSettings : ScriptableObject
    {
        
        public string serverAddress = "https://localhost:5001";
        public float cooldownBeforeSend = 3f;
    }
}