using System;
using Source.Core.Components;

namespace Source.Core
{
    public sealed class UnityScheduler : MonoBehaviorSingleton<UnityScheduler>
    {
        public event Action tickCallback;
        
        private void Update()
        {
            tickCallback?.Invoke();
        }
        
        protected override void OnDestroy()
        {
            base.OnDestroy();
            tickCallback = null;
        }
    }
}