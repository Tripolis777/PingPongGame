using UnityEngine;

namespace Source.Core.Input
{
    public struct InputAction
    {
        public InputActionType actionType;
        public Vector3 position;
        public Vector3 lastPosition;
        public KeyCode keyCode;
    }

    public enum InputActionType
    {
        KeyPress,
        Touch,
        Swipe,
    }
}