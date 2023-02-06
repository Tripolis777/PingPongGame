using System;
using UnityEngine;

namespace Source.Gameplay.Player
{
    public interface IPlayerInput : IDisposable, IPausable
    {
        event Action<Vector3> onChangePosition;
    }
}