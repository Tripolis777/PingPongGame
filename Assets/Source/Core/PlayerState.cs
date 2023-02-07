using System;
using UnityEngine;

namespace Source.Core
{
    public class PlayerState
    {
        public Color ballColor;
        public int ballViewIndex;
        public int bestScore;

        public event Action<PlayerState> OnChanged;

        public void UpdateBestScore(int bestScore)
        {
            this.bestScore = bestScore;
            OnChanged?.Invoke(this);
        }
    }
}