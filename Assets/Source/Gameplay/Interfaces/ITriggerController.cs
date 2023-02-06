using UnityEngine;

namespace Source.Gameplay
{
    public interface ITriggerController : IGameplayController
    {
        void OnTriggered(Collider collider);
    }
}