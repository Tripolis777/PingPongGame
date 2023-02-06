using System;
using UnityEngine;

namespace Source.Core
{
    [Serializable]
    public class WorldCameraController
    {
        [SerializeField]
        private Camera gameCamera;
        [SerializeField]
        private Camera uiCamera;

        public Camera GetGameCamera() => gameCamera;
        
        public Camera GetUICamera() => uiCamera;
    }
}