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
        [SerializeField] private PlayerInfo[] players;

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
            }
            
            return UniTask.CompletedTask;
        }

        [Serializable]
        public struct PlayerInfo
        {
            public PlayerViewComponent viewComponent;
        }
    }
}