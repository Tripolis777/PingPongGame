using System;
using UnityEngine;

namespace Source.Gameplay
{
    public class TriggerController : GameplayViewController<TriggerWrapperView>
    {
        public event Action<int> OnScoreCollected;
        private int _playerIndex;
        private GameObject _target; 

        public TriggerController(TriggerWrapperView view, int playerIndex) : base(view)
        {
            _playerIndex = playerIndex;
        }

        public void SetTarget(GameObject target) {}
        
        public void OnBallCollision()
        {
               
        }
    }
}