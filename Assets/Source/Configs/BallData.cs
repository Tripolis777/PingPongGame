using UnityEngine;

namespace Source.Configs
{
    [CreateAssetMenu(fileName = "Ball Data", menuName = "Configs/Ball Data")]
    public class BallData : ScriptableObject
    {
        public Vector3 maxRandomVelocity;
        public float speed;
    }
}