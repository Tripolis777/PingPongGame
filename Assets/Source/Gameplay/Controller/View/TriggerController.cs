using System;
using System.Collections.Generic;
using UnityEngine;

namespace Source.Gameplay
{
    public class TriggerController<T> : GameplayViewController<TriggerWrapperView>, ITriggerController
    {
        public event Action<T> OnTriggerEnter;
        private T _triggerData;
        private HashSet<GameObject> _targets = new HashSet<GameObject>(); 

        public TriggerController(TriggerWrapperView view, T triggerData) : base(view)
        {
            _triggerData = triggerData;
        }

        public void AddTarget(GameObject target) => _targets.Add(target);

        public void OnTriggered(Collider collider)
        {
            if (!_targets.Contains(collider.gameObject))
                return;
            
            OnTriggerEnter?.Invoke(_triggerData);
        }

        protected override void DisposeInternal()
        {
            base.DisposeInternal();
            _targets.Clear();
            _triggerData = default;
            OnTriggerEnter = null;
        }
    }
}