using System;
using Source.Configs;

namespace Source.Gameplay
{
    [Serializable]
    public struct PlayerGameState
    {
        public PlayerConfig playerData;
        public int score;
    }
}