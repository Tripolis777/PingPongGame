using Source.Gameplay.GameComponents;
using Source.View;
using UnityEngine;

namespace Source.Gameplay
{
    [RequireComponent(typeof(Collider))]
    public class TriggerWrapperView : ViewComponent<ITriggerController>
    {
        private BallComponent ball;
        
        public override void Init(GameplayController controller)
        {
            base.Init(controller);
            ball = BallComponent.Get();
        }

        private void OnTriggerEnter(Collider other) => controller.OnTriggered(other);
    }
}