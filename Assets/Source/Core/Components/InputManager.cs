using System.Collections.Generic;
using Source.Configs;
using Source.Core.Components;
using UnityEngine;

namespace Source.Core.Input
{
    public sealed class InputManager : MonoBehaviorSingleton<InputManager>
    {
        [SerializeField] private InputConfig inputConfig;

        private Dictionary<int, Touch> _currentTouches = new Dictionary<int, Touch>();
        private Vector3 _startMousePosition;
        private bool _isMouseMoved;
        private Vector3 _lastPosition;
        private IInputService _inputService;

        protected override void Awake()
        {
            _inputService = ServiceLocator.Instance.GetService<IInputService>();
            Debug.Log($"Touch enables: {UnityEngine.Input.touchSupported} and simulate is {UnityEngine.Input.simulateMouseWithTouches}"); 
        }

        private void Update()
        {
            if (UnityEngine.Input.touchSupported)
                CalculateTouches();
            else 
                CalculateMouse();
        }
        
        private void CalculateTouches()
        {
            foreach (var touch in UnityEngine.Input.touches)
            {
                if (touch.phase == TouchPhase.Began)
                {
                    Debug.Log("Start began");
                    _currentTouches.Add(touch.fingerId, touch);
                }

                if (touch.phase == TouchPhase.Moved)
                {
                    Debug.Log($"Touch Delta = {touch.deltaPosition}");
                    if(CheckSwipe(touch.deltaPosition))
                        _inputService.SendAction(new InputAction()
                        {
                            actionType = InputActionType.Swipe,
                            position = touch.position,
                            lastPosition = touch.position - touch.deltaPosition,
                        });
                }

                if (touch.phase == TouchPhase.Canceled)
                {
                    _currentTouches.Remove(touch.fingerId);
                }
            }
        }

        private void CalculateMouse()
        {
            if (_isMouseMoved)
            {
                var mousePosition = UnityEngine.Input.mousePosition;
                var isSwipe = CheckSwipe(mousePosition - _startMousePosition);
                if (isSwipe)
                    _inputService.SendAction(new InputAction()
                    {
                        actionType = InputActionType.Swipe,
                        position = mousePosition,
                        lastPosition = _lastPosition
                    });

                _lastPosition = mousePosition;
            }
            
            if (UnityEngine.Input.GetMouseButtonDown(0))
            {
                _startMousePosition = UnityEngine.Input.mousePosition;
                _isMouseMoved = true;
            }

            if (UnityEngine.Input.GetMouseButtonUp(0))
            {
                _isMouseMoved = false;
                _lastPosition = Vector3.zero;
            }
        }

        private bool CheckSwipe(Vector2 delta) => Mathf.Abs(delta.x) > inputConfig.swipeThreshold ||
                                               Mathf.Abs(delta.y) > inputConfig.swipeThreshold;

        private void OnDisable()
        {
            _currentTouches.Clear();
        }
    }
}