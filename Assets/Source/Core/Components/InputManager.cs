using System.Collections.Generic;
using Source.Configs;
using Source.Core.Components;
using UnityEngine;

namespace Source.Core.Input
{
    public sealed class InputManager : MonoBehaviorSingleton<InputManager>
    {
        [SerializeField]
        private InputConfig inputConfig;

        private Dictionary<int, Touch> currentTouches = new Dictionary<int, Touch>();
        private Vector3 startMousePosition;
        private bool isMouseMoved;
        private Vector3 lastPosition;
        private IInputService inputService;

        protected override void Awake()
        {
            inputService = ServiceLocator.Instance.GetService<IInputService>();
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
                    currentTouches.Add(touch.fingerId, touch);
                }

                if (touch.phase == TouchPhase.Moved)
                {
                    Debug.Log($"Touch Delta = {touch.deltaPosition}");
                    if(CheckSwipe(touch.deltaPosition))
                        inputService.SendAction(new InputAction()
                        {
                            actionType = InputActionType.Swipe,
                            position = touch.position,
                            lastPosition = touch.position - touch.deltaPosition,
                        });
                }

                if (touch.phase == TouchPhase.Canceled)
                {
                    currentTouches.Remove(touch.fingerId);
                }
            }
        }

        private void CalculateMouse()
        {
            if (isMouseMoved)
            {
                var mousePosition = UnityEngine.Input.mousePosition;
                var isSwipe = CheckSwipe(mousePosition - startMousePosition);
                if (isSwipe)
                    inputService.SendAction(new InputAction()
                    {
                        actionType = InputActionType.Swipe,
                        position = mousePosition,
                        lastPosition = lastPosition
                    });

                lastPosition = mousePosition;
            }
            
            if (UnityEngine.Input.GetMouseButtonDown(0))
            {
                startMousePosition = UnityEngine.Input.mousePosition;
                isMouseMoved = true;
            }

            if (UnityEngine.Input.GetMouseButtonUp(0))
            {
                isMouseMoved = false;
                lastPosition = Vector3.zero;
            }
        }

        private bool CheckSwipe(Vector2 delta) => Mathf.Abs(delta.x) > inputConfig.swipeThreshold ||
                                               Mathf.Abs(delta.y) > inputConfig.swipeThreshold;

        private void OnDisable()
        {
            currentTouches.Clear();
        }
    }
}