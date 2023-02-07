using Source.Gameplay;
using TMPro;
using UnityEngine;

namespace Source.View
{
    public class ScorePresenterView : ViewComponent<ScorePresenterController>
    {
        [SerializeField] private TMP_Text scoreText;

        public void SetScore(int playerScore, int opponentScore)
        {
            scoreText.text = $"{playerScore} : {opponentScore}";
        }
    }
}