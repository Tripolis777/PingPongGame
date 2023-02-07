using Source.Gameplay.Controller.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Source.View
{
    public class GameUIOverlayView : ViewComponent<GameUIOverlayController>
    {
        [SerializeField] private TMP_Text bestScoreText;
        [SerializeField] private Button pauseButton;

        private void Awake() => pauseButton.onClick.AddListener(OnPauseButtonClick);

        public void SetBestScore(int score) => bestScoreText.text = score.ToString();
        
        private void OnPauseButtonClick() => controller?.PauseClick();
    }
}