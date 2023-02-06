using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Source.Core;
using Source.View;
using UnityEngine;

namespace Source.Gameplay.GameComponents
{
    public class ScoreComponent : GameComponent<ScoreComponent>
    {
        [SerializeField] private GameObject ballTarget; 
        [SerializeField] private TriggerWrapperView[] scoreTriggers;
        [SerializeField] private ScorePresenterView scorePresenterView;
        [SerializeField] private int scoreDelta = 1;
        
        private List<TriggerController<int>> _triggerControllers;
        private GameController _gameController;

        public override UniTask LoadComponent(GameScene gameScene)
        {
            _gameController = gameScene as GameController;
            var gameState = _gameController.GetGameState();
            _triggerControllers = new List<TriggerController<int>>(gameState.players.Length);

            for (var i = 0; i < gameState.players.Length; i++)
            {
                var scoreView = scoreTriggers[i];
                var controller = new TriggerController<int>(scoreView, i);
                _triggerControllers.Add(controller);
                AddController(controller);
            }
          
            AddController(new ScorePresenterController(scorePresenterView, _gameController));
            return UniTask.CompletedTask;
        }

        public override void OnGameLoaded()
        {
            base.OnGameLoaded();
            foreach (var triggerController in _triggerControllers)
            {
                triggerController.AddTarget(ballTarget);
                triggerController.OnTriggerEnter += OnBallMissed;
            }
        }

        public override void Dispose()
        {
            foreach (var triggerController in _triggerControllers)
                triggerController.OnTriggerEnter -= OnBallMissed;

            _triggerControllers.Clear();
            base.Dispose();
        }

        private void OnBallMissed(int playerIndex) => _gameController.ChangePlayerScore(playerIndex, scoreDelta);
    }
}