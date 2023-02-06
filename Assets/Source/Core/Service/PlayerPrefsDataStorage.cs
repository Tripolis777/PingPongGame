using System;
using UnityEngine;

namespace Source.Core.Service
{
    public class PlayerPrefsDataStorage : IStorageDataService
    {
        public bool TryLoadData<T>(out T data)
        {
            data = default;
            
            var key = GetKey<T>();
            var serialized = PlayerPrefs.GetString(key);
            if (string.IsNullOrEmpty(serialized))
                return false;

            var type = typeof(T);
            if (typeof(ScriptableObject).IsAssignableFrom(type))
            {
                var instance = ScriptableObject.CreateInstance(type);
                JsonUtility.FromJsonOverwrite(serialized, instance);
                data = (T) Convert.ChangeType(instance, type);
            } 
            else
                data = JsonUtility.FromJson<T>(serialized);
            
            return true;
        }
        
        public void SaveData<T>(T data)
        {
            var key = GetKey<T>();
            var serialized = JsonUtility.ToJson(data);
            PlayerPrefs.SetString(key, serialized);
            PlayerPrefs.Save();
        }

        private string GetKey<T>() => typeof(T).Name;
    }
}