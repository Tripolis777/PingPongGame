using UnityEngine;

namespace Source.Configs
{
    [CreateAssetMenu(fileName = "GlobalConfig", menuName = "Configs/Global Config")]
    public class GlobalConfigs : ScriptableObject
    {
        private const string GLOBAL_CONFIG_PATH = "Assets/Resources/Configs/GlobalConfig.asset";

        private static GlobalConfigs _instance;
        public static GlobalConfigs Instance
        {
            get
            {
                if (_instance == null)
                    _instance = Resources.Load<GlobalConfigs>(GLOBAL_CONFIG_PATH);
                
                return _instance;
            }
        }

        [Header("Ball Settings")]
        public BallData ballData;
        public GameObject[] ballViews;
    }
}