using System;
using UnityEngine;

namespace Source.Core.Input
{
    public struct Swipe
    {
        public SwipeDirection direction;
        public Vector2 delta;
    }

    [Flags]
    public enum SwipeDirection
    {
        None = 0,
        Left = 1 << 0,
        Right = 1 << 1,
        Up = 1 << 2,
        Down = 1 << 3,
    }
}