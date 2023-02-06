using UnityEngine;

namespace Source.Configs
{
    [CreateAssetMenu(fileName = "Input Config", menuName = "Configs/InputConfig")]
    public sealed class InputConfig : ScriptableObject
    {
        public float swipeThreshold = 10f;
    }
}