using Source.Gameplay;
using UnityEngine;

namespace Source.View
{
    public abstract class ViewComponent : MonoBehaviour
    {
        public abstract void Init(GameplayController controller);
    }
    
    public abstract class ViewComponent<T> : ViewComponent where T : class, IGameplayController
    {
        protected T controller { get; private set; }

        public override void Init(GameplayController controller) => this.controller = controller as T;

        private void OnDestroy()
        {
            if (controller == null)
                return; 
            
            controller.Dispose();
            controller = null;
        }
    }
}