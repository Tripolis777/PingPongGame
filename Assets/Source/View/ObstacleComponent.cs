using UnityEngine;

namespace Source.Gameplay
{
    public class ObstacleComponent : MonoBehaviour, IBallInteractable
    {
        private float interactionForce;
        
        public Vector3 GetForce() => Vector3.zero;
    }
}