using System;
using System.Collections;
using System.Collections.Generic;
using CustomGameAnalytics.Scripts.Common;
using CustomGameAnalytics.Scripts.Events;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UIElements;

namespace CustomGameAnalytics.Scripts
{
    internal class EventsSender
    {
        private readonly StorageEvents _storageEvents;
        private readonly StorageSendingEvents _storageSendingEvents;
        private readonly string _url;

        public EventsSender(string url,float cooldownBeforeSend)
        {
            _url = url;
            _storageSendingEvents = new StorageSendingEvents();
            _storageEvents = new StorageEvents();
            var pendingEvents = _storageSendingEvents.GetFromDisk();
            if (pendingEvents != null) _storageEvents.StoreEvent(pendingEvents);

            CoroutineManager.Instance.StartCoroutine(EventsSend(cooldownBeforeSend));
        }

        public void AddEvent(CustomEvent customEvent)
        {
            _storageEvents.StoreEvent(customEvent);
        }

        private IEnumerator EventsSend(float cooldownBeforeSend)
        {
            var waitInterval = new WaitForSeconds(cooldownBeforeSend);
            while (true)
            {
                yield return waitInterval;
                
                var list = _storageEvents.GetEvents();
                if(list.Count<0) continue;
                
                var id = _storageSendingEvents.StoreEvent(list);
                EventPackage pkg = new EventPackage{events = list};
                var evStr = JsonConvert.SerializeObject(pkg);
                
                CoroutineManager.Instance.StartCoroutine(PostEvents(id, evStr, OnSuccess, OnError));
            }
        }

        private void OnError(string id)
        {
            _storageEvents.StoreEvent(_storageSendingEvents.GetEvents(id));
            _storageSendingEvents.RemoveEvent(id);
        }

        private void OnSuccess(string id)
        {
            _storageSendingEvents.RemoveEvent(id);
        }

        private IEnumerator PostEvents(string id, string dataJson, Action<string> onSuccess, Action<string> onError)
        {
            WWWForm form = new WWWForm();
            form.AddField("data", dataJson);

            using (UnityWebRequest www = UnityWebRequest.Post(_url, form))
            {
                yield return www.SendWebRequest();

                if (www.isNetworkError || www.isHttpError || www.responseCode != 200)
                {
                    onError?.Invoke(id);
                }
                else
                {
                    onSuccess?.Invoke(id);
                }
            }
        }
    }
}