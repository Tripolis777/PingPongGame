using Source.Core.Service;

namespace Source.Gameplay
{
    public class BestScoreController : GameplayController
    {
        private GameController _gameController;
        private IPlayerStateService _playerStateService;
        private int _lastBestScore;

        public BestScoreController(GameController gameController, IPlayerStateService playerStateService)
        {
            _gameController = gameController;
            _playerStateService = playerStateService;
        }
        
        public override void Init()
        {
            var playerState = _playerStateService.GetState();
            _lastBestScore = playerState.bestScore;
            
            _gameController.OnGameStateChange += OnGameStateChanged;
        }

        protected override void DisposeInternal()
        {
            base.DisposeInternal();
            _gameController.OnGameStateChange -= OnGameStateChanged;
            _playerStateService.SaveState();

            _gameController = null;
            _playerStateService = null;
        }

        private void OnGameStateChanged(GameState gameState)
        {
            var newScore = gameState.MainPlayer.score;
            if (_lastBestScore >= newScore)
                return;

            var playerState = _playerStateService.GetState();
            playerState.UpdateBestScore(newScore);
            _lastBestScore = newScore;
        }
    }
}