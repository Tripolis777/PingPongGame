using Cysharp.Threading.Tasks;
using Source.Configs;
using Source.Core;
using UnityEngine;

namespace Source.Gameplay.GameComponents
{
    public class BallComponent : GameComponent<BallComponent>
    {
        [SerializeField]
        private BallViewComponent ball;
        [SerializeField]
        private Transform ballStartPivot;

        private BallController _ballController;
        
        public override UniTask LoadComponent(GameScene gameScene)
        {
            _ballController = new BallController(ball, GlobalConfigs.Instance.ballData);
            _controllers.Add(_ballController);

            return UniTask.CompletedTask;
        }

        public void ResetState()
        {
            _ballController.SetToPosition(ballStartPivot);
            _ballController.AddRandomDirection();
        }

        public GameObject GetBallObject() => ball.gameObject;
    }
}