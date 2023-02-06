using UnityEngine;

namespace Source.Core.Components
{
    public abstract class MonoBehaviorSingleton<T> : MonoBehaviour where T : class
    {
        public static T Instance { get; private set; }

        protected virtual void Awake()
        {
            Instance = this as T;
            DontDestroyOnLoad(this.gameObject);
        }

        protected virtual void OnDestroy()
        {
            Instance = null;
        }
    }
}