using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Source.Core;
using Source.Gameplay.Player;
using Source.View;
using UnityEngine;

namespace Source.Gameplay.GameComponents
{
    public class PlayersGameComponent : GameComponent<PlayersGameComponent>
    {
        [SerializeField]
        private PlayerInfo[] players;

        private List<TriggerController> scoreControllers = new List<TriggerController>();

        public event Action<int> OnBallMissed;

        public override UniTask LoadComponent(GameScene gameScene)
        {
            if (players.Any(x => x.viewComponent == null))
            {
                Debug.LogError("Cannot find player views.");
                return UniTask.CompletedTask;
            }

            if (gameScene is not GameController gameController)
            {
                Debug.LogError($"{nameof(PlayersGameComponent)} need {nameof(GameController)} game scene.");
                return UniTask.CompletedTask;
            }

            var gameState = gameController.GetGameState();
            for (var i = 0; i < players.Length; i++)
            {
                var playerInfo = players[i];
                _controllers.Add(new PlayerController(playerInfo.viewComponent, gameState.players[i].playerData));
             
                if (playerInfo.scoreCollector)
                {
                    var scoreController = new TriggerController(playerInfo.scoreCollector, i);
                    scoreController.OnScoreCollected += OnScoreCollected;
                    scoreControllers.Add(scoreController);
                }
            }
            
            return UniTask.CompletedTask;
        }

        public override void OnGameLoaded()
        {
            base.OnGameLoaded();
            foreach (var scoreController in scoreControllers)
            {
                
            }
        }

        private void OnScoreCollected(int playerIndex) => OnBallMissed?.Invoke(playerIndex);
        
        [Serializable]
        public struct PlayerInfo
        {
            public PlayerViewComponent viewComponent;
            public TriggerWrapperView scoreCollector;
        }
    }
}