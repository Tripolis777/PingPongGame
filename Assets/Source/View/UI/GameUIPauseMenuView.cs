using Source.Gameplay.Controller.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Source.View
{
    public class GameUIPauseMenuView : ViewComponent<GameUIMenuController>
    {
        [SerializeField] private Button resumeButton;
        [SerializeField] private Button exitButton;

        private void Awake()
        {
            resumeButton.onClick.AddListener(OnResumeClick);
            exitButton.onClick.AddListener(OnExitClick);
        }

        private void OnResumeClick() => controller.ResumeClick();

        private void OnExitClick() => controller.ExitClick();
    }
}