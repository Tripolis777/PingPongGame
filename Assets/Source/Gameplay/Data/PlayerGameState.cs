using System;
using Source.Configs;

namespace Source.Gameplay
{
    [Serializable]
    public class PlayerGameState
    {
        public PlayerConfig playerData;
        public int score;
    }
}