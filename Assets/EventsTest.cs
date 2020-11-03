using System;
using CustomGameAnalytics;
using CustomGameAnalytics.Scripts;
using CustomGameAnalytics.Scripts.Events;
using CustomGameAnalytics.Scripts.Events.Data;
using UnityEngine;

public class EventsTest : MonoBehaviour
{
    private void Start()
    {
        EventsManager.TrackEvent(new LevelInfoData(LevelInfoType.Lose, 1){});
        EventsManager.TrackEvent(new LevelInfoData(LevelInfoType.Start, 1));
        EventsManager.TrackEvent(new LevelInfoData(LevelInfoType.Win, 1));
        EventsManager.TrackEvent(new PaymentData(){price = 11, paymentId = Guid.NewGuid().ToString()});
    }
}