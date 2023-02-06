using System;
using Cysharp.Threading.Tasks;
using Source.Core;
using Source.Core.Service;
using Source.Gameplay.GameComponents;
using UnityEngine;

namespace Source.Gameplay
{
    public class GameController : GameScene
    {
        [SerializeField] private GameState gameState;

        private BallComponent ballComponent;
        private IPlayerStateService playerStateService;
        private BestScoreController bestScoreController;

        public event Action<GameState> OnGameStateChange;

        public override async UniTask Init()
        {
            await base.Init();

            playerStateService = ServiceLocator.Instance.GetService<IPlayerStateService>();
            bestScoreController = new BestScoreController(this, playerStateService);
            bestScoreController.Init();
            
            ballComponent = GetGameComponent<BallComponent>();
            StartGame();
        }

        protected override void AddGameComponents()
        {
            //gameObject.GetOrAddComponent<PlayersGameComponent>();
        }

        public GameState GetGameState() => gameState;

        public void ChangePlayerScore(int playerIndex, int deltaScore)
        {
            var playerInfo = gameState.players[playerIndex];
            playerInfo.score += deltaScore;
            OnGameStateChange?.Invoke(gameState);
            
            StartGame();
        }

        public void StartGame()
        {
            ballComponent.ResetState();
        }

        private void OnDestroy()
        {
            playerStateService.SaveState();
            bestScoreController.Dispose();
            bestScoreController = null;
        }
    }
}