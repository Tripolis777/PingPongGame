using Source.Core;
using Source.Gameplay.Player;
using UnityEngine;

namespace Source.Configs
{
    [CreateAssetMenu(fileName = "Player Settings", menuName = "Configs/PlayerSettings")]
    public class PlayerConfig : ScriptableObject
    {
        public float velocityFadeTime;
        
        [SelectImplementation(typeof(PlayerInputSettings))]
        [SerializeReference]
        public PlayerInputSettings inputSettings;
    }
}