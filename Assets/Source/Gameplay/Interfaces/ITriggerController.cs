using UnityEngine;

namespace Source.Gameplay
{
    public interface ITriggerController : IGameplayController
    {
        void OnTriggerEnter(Collider colider);
    }
}