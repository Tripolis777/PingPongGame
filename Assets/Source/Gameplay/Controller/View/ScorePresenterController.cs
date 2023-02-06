using Source.View;

namespace Source.Gameplay
{
    public class ScorePresenterController : GameplayViewController<ScorePresenterView>
    {
        private GameController _gameController;
        
        public ScorePresenterController(ScorePresenterView view, GameController gameController) : base(view)
        {
            _gameController = gameController;
        }

        public override void Init()
        {
            base.Init();
            _gameController.OnGameStateChange += OnGameStateChange;
            OnGameStateChange(_gameController.GetGameState());
        }

        protected override void DisposeInternal()
        {
            base.DisposeInternal();
            if (_gameController == null)
                return; 
            
            _gameController.OnGameStateChange -= OnGameStateChange;
            _gameController = null;
        }

        private void OnGameStateChange(GameState gameState) =>
            View.SetScore(gameState.MainPlayer.score, gameState.OpponentPlayer.score);
    }
}