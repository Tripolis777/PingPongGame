using Cysharp.Threading.Tasks;
using Source.Core;
using Source.Core.Service;
using Source.Gameplay.Controller.UI;
using Source.View;
using UnityEngine;

namespace Source.Gameplay.GameComponents
{
    public class UIGameComponent : GameComponent<UIGameComponent>
    {
        [SerializeField] private GameUIOverlayView gameOverlayView;
        [SerializeField] private GameUIPauseMenuView gameUIPauseMenuView;

        private GameController _gameController;
        
        public override UniTask LoadComponent(GameScene gameScene)
        {
            _gameController = gameScene as GameController;

            var playerStateService = ServiceLocator.Instance.GetService<IPlayerStateService>();
            AddController(new GameUIOverlayController(gameOverlayView, playerStateService, _gameController));
            AddController(new GameUIMenuController(gameUIPauseMenuView, _gameController));

            return UniTask.CompletedTask;
        }

        public override void Dispose()
        {
            base.Dispose();
            _gameController = null;
        }
    }
}