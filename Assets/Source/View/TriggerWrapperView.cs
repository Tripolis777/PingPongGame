using Source.View;
using UnityEngine;

namespace Source.Gameplay
{
    [RequireComponent(typeof(Collider))]
    public class TriggerWrapperView : ViewComponent<ITriggerController>
    {
        private void OnTriggerEnter(Collider other) => controller.OnTriggered(other);
    }
}