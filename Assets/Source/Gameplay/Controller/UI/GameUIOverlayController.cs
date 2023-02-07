using Source.Core;
using Source.Core.Service;
using Source.View;

namespace Source.Gameplay.Controller.UI
{
    public class GameUIOverlayController : GameplayViewController<GameUIOverlayView>
    {
        private IPlayerStateService _playerStateService;
        private GameController _gameController;

        public GameUIOverlayController(GameUIOverlayView view, IPlayerStateService playerStateService, GameController gameController) : base(view)
        {
            _playerStateService = playerStateService;
            _gameController = gameController;
        }
        
        public override void Init()
        {
            base.Init();
            var playerState = _playerStateService.GetState();
            playerState.OnChanged += UpdatePlayerState;
            UpdatePlayerState(playerState);
        }
        
        protected override void DisposeInternal()
        {
            base.DisposeInternal();
            var playerState = _playerStateService.GetState();
            playerState.OnChanged -= UpdatePlayerState;
            _gameController = null;
            _playerStateService = null;
        }

        public void PauseClick() => _gameController.Pause();
        
        private void UpdatePlayerState(PlayerState state) => View.SetBestScore(state.bestScore);
    }
}