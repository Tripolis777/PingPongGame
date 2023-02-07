using Source.View;
using UnityEngine;

namespace Source.Gameplay.Controller.UI
{
    public class GameUIMenuController : GameplayViewController<GameUIPauseMenuView>
    {
        private GameController _gameController;

        public GameUIMenuController(GameUIPauseMenuView view, GameController gameController) : base(view)
        {
            _gameController = gameController;
        }

        public override void Init()
        {
            base.Init();
            View.Hide();
        }

        protected override void DisposeInternal()
        {
            base.DisposeInternal();
            _gameController = null;
        }

        public void ResumeClick() => _gameController.Resume();

        public void ExitClick() => _gameController.QuitGame();

        protected override void OnPause()
        {
            base.OnPause();
            View.Show();
        }

        protected override void OnResume()
        {
            base.OnResume();
            View.Hide();
        }
    }
}