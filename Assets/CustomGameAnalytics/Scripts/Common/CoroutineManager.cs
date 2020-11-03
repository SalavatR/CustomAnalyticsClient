using System;
using System.Collections;
using UnityEngine;

namespace CustomGameAnalytics.Scripts.Common
{
    public class CoroutineManager : Singleton<CoroutineManager>
    {

        private void Awake()
        {
            if (Instance != this)
            {
                Destroy(this);
                Debug.LogError("Only one instance can be on scene");
            }
        }

        public new Coroutine StartCoroutine(IEnumerator coroutine)
        {
            if (coroutine == null) throw new Exception();
            return base.StartCoroutine(coroutine);
        }

        public new void StopCoroutine(Coroutine coroutine)
        {
            if (coroutine == null) throw new Exception();
            base.StopCoroutine(coroutine);
        }
    }
}