using Cysharp.Threading.Tasks;
using Source.Core;
using Source.Core.Service;
using Source.Gameplay.GameComponents;
using UnityEngine;

namespace Source.Gameplay
{
    public class GameController : GameScene
    {
        [SerializeField]
        private GameState gameState;

        private BallComponent ballComponent;
        private PlayerState playerState;
        private IPlayerStateService playerStateService;

        public override async UniTask Init()
        {
            await base.Init();

            playerStateService = ServiceLocator.Instance.GetService<IPlayerStateService>();
            playerState = playerStateService.GetState();

            ballComponent = GetGameComponent<BallComponent>();
            StartGame();
        }

        protected override void AddGameComponents()
        {
            //gameObject.GetOrAddComponent<PlayersGameComponent>();
        }

        public GameState GetGameState() => gameState;

        public void StartGame()
        {
            ballComponent.ResetState();
        }

        private void OnDestroy()
        {
            playerStateService.SaveState();
        }
    }
}