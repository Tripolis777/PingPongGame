using System;
using Source.Core;
using Source.Core.Input;
using UnityEngine;

namespace Source.Gameplay.Player
{
    [Serializable]
    public abstract class PlayerInputSettings
    {
    }

    [Serializable]
    public class PlayerAISettings : PlayerInputSettings
    {
    }

    [Serializable]
    public class PlayerHumanSettings : PlayerInputSettings
    {
        public Camera gameCamera => WorldController.Instance.WorldCameraController.GetGameCamera();
        public IInputService inputService => ServiceLocator.Instance.GetService<IInputService>();
    }
}