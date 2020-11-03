using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace CustomGameAnalytics.Scripts.Common
{
    
#if !UNITY_WEBGL
    internal class LocalInfoStorage<T> : ILocalInfoStorage<T>
    {
        private readonly string _path;
        private readonly BinaryFormatter serializer = new BinaryFormatter();

        public LocalInfoStorage(string key)
        {
            _path = Path.Combine(Application.persistentDataPath, $"{key}.dat");
        }
        
        
        public void Save(T obj)
        {
            using (var fs = File.Create(_path))
            {
                serializer.Serialize(fs, obj);
            }
        }

        public T Load()
        {
            if (!File.Exists(_path)) return default(T);
            using (var fs = File.Open(_path, FileMode.Open))
            {
                return (T) serializer.Deserialize(fs);
            }
        }
#endif
    }
}