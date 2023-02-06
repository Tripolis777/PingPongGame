using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Source.Core
{
    public abstract class GameScene : MonoBehaviour
    {
        [SerializeField]
        private Transform cameraPivot;
        
        protected Camera gameCamera;

        protected Dictionary<Type, GameComponent> componentsByType = new Dictionary<Type, GameComponent>();

        public virtual async UniTask Init()
        {
            gameCamera = WorldController.Instance.WorldCameraController.GetGameCamera();
            gameCamera.transform.SetPositionAndRotation(cameraPivot.position, transform.rotation);

            AddGameComponents();
            await InitGameComponents();
        }

        public T GetGameComponent<T>() where T : GameComponent
        {
            if (componentsByType.TryGetValue(typeof(T), out var component))
                return component as T;

            Debug.LogError($"Component of type {typeof(T)} not found.");
            return null;
        }
        
        protected abstract void AddGameComponents();

        private async UniTask InitGameComponents()
        {
            componentsByType.Clear();
            foreach (var component in FindObjectsOfType<GameComponent>())
            {
                var type = component.GetType();
                if (!componentsByType.TryGetValue(type, out var gameComponent))
                    componentsByType[type] = component;
                else
                    Debug.LogError($"Multiple GameComponents of type {type} - {component.gameObject}");
            }

            await UniTask.WhenAll(componentsByType.Values.Select(x => x.LoadComponent(this)));

            foreach (var component in componentsByType.Values)
                component.OnGameLoaded();
        }
    }
}