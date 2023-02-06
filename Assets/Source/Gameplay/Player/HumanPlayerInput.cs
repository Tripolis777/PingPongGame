using System;
using Source.Core.Input;
using UnityEngine;

namespace Source.Gameplay.Player
{
    public class HumanPlayerInput : IPlayerInput
    {
        private IInputService _inputService;
        private Camera _cam;
        private float _distanceToField;
        private bool isPaused;

        public event Action<Vector3> onChangePosition;
        
        public HumanPlayerInput(PlayerHumanSettings settings, float distanceToField)
        {
            _inputService = settings.inputService;
            _inputService.onAction += OnInputAction;
            _cam = settings.gameCamera;
            _distanceToField = distanceToField;
        }

        private void OnInputAction(InputAction action)
        {
            if (isPaused)
                return;
            
            if (action.actionType != InputActionType.Swipe)
                return;
                
            var worldPosition = _cam.ScreenToWorldPoint(new Vector3(action.position.x, action.position.y, _distanceToField));
            var lastWorldPosition = _cam.ScreenToWorldPoint(new Vector3(action.lastPosition.x, action.lastPosition.y, _distanceToField));

            var delta = worldPosition - lastWorldPosition;
            var position = new Vector3(delta.x, 0, 0);
            onChangePosition?.Invoke(position);
        }

        public void Dispose()
        {
            _inputService.onAction -= OnInputAction;
            _inputService = null;
            _cam = null;
        }

        public void Pause() => isPaused = true;

        public void Resume() => isPaused = false;
    }
}