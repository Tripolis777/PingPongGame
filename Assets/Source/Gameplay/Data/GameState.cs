using System;

namespace Source.Gameplay
{
    [Serializable]
    public class GameState
    {
        public PlayerGameState[] players;

        public PlayerGameState MainPlayer => players[0];
        public PlayerGameState OpponentPlayer => players[1];
    }
}