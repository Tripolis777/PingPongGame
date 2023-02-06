using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Source.Gameplay;
using UnityEngine;

namespace Source.Core
{
    public abstract class GameComponent : MonoBehaviour, IPausable
    {
        protected List<IGameplayController> _controllers = new List<IGameplayController>();

        public abstract UniTask LoadComponent(GameScene gameScene);

        public virtual void OnGameLoaded()
        {
            foreach (var controller in _controllers)
                controller.Init();
        }
        
        public void Pause()
        {
            foreach (var controller in _controllers.OfType<IPausable>())
                controller.Pause();
        }

        public void Resume()
        {
            foreach (var controller in _controllers.OfType<IPausable>())
                controller.Resume();
        }
    }

    public abstract class GameComponent<T> : GameComponent where T : GameComponent
    {
        public static T Get()
        {
            if (WorldController.Instance == null || WorldController.Instance.currentScene == null)
                return null;

            return WorldController.Instance.currentScene.GetGameComponent<T>();
        } 
    }
}