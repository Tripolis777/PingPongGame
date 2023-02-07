using System;
using Cysharp.Threading.Tasks;
using Source.Core;
using Source.Core.Service;
using Source.Gameplay.GameComponents;
using UnityEditor;
using UnityEngine;

namespace Source.Gameplay
{
    public class GameController : GameScene, IPausable
    {
        [SerializeField] private GameState gameState;

        private BallComponent _ballComponent;
        private IPlayerStateService _playerStateService;
        private BestScoreController _bestScoreController;

        public event Action<GameState> OnGameStateChange;
        private float lastTimeScale;

        public override async UniTask Init()
        {
            await base.Init();

            _playerStateService = ServiceLocator.Instance.GetService<IPlayerStateService>();
            _bestScoreController = new BestScoreController(this, _playerStateService);
            _bestScoreController.Init();
            
            _ballComponent = GetGameComponent<BallComponent>();
            StartGame();
        }

        protected override void AddGameComponents()
        {
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
            _ballComponent.ResetState();
        }

        public void Pause()
        {
            foreach (var component in componentsByType.Values)
                component.Pause();
            
            lastTimeScale = Time.timeScale;
            Time.timeScale = 0f;
        }

        public void Resume()
        {
            foreach (var component in componentsByType.Values)
                component.Resume();
            
            Time.timeScale = lastTimeScale;
        }

        public void QuitGame()
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
            return;
#endif
            Application.Quit();
        }
        
        private void OnDestroy()
        {
            _playerStateService.SaveState();
            _bestScoreController.Dispose();
            _bestScoreController = null;
        }
    }
}