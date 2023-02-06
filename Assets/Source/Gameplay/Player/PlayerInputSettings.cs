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
        public string testString;
    }

    [Serializable]
    public class PlayerHumanSettings : PlayerInputSettings
    {
        public int testNumber;
        
        public Camera gameCamera => WorldController.Instance.WorldCameraController.GetGameCamera();
        public IInputService inputService => ServiceLocator.Instance.GetService<IInputService>();
    }
}