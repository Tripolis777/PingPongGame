using System;

namespace Source.Gameplay
{
    [Serializable]
    public struct GameState
    {
        public PlayerGameState[] players;
    }
}