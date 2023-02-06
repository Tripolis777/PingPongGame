using Source.Configs;
using Source.Core;
using Source.Core.Service;
using UnityEngine;

namespace Source.Gameplay
{
    public class BallController : GameplayViewController<BallViewComponent>
    {
        private BallData _data;

        public BallController(BallViewComponent view, BallData data) : base(view)
        {
            _data = data;
        }

        public override void Init()
        {
            base.Init();
            InitializeBallView();
            View.SetSpeed(_data.speed);
        }
        
        public void SetView(GameObject ballView) => View.LoadView(ballView);

        public void SetToPosition(Transform pivot) =>
            View.transform.SetPositionAndRotation(pivot.position, pivot.rotation);

        public void AddRandomDirection() =>
            View.SetDirection(new Vector3(Random.Range(0f, 1f), 0f, Random.Range(0f, 1f)));

        public void OnCollision(Vector3 direction, Collision collision)
        {
            var contact = collision.GetContact(0);
            var newDirection = Vector3.Reflect(direction, contact.normal);
            
            View.SetDirection(newDirection);
        }
        
        private void InitializeBallView()
        {
            var globalConfig = GlobalConfigs.Instance;
            var stateService = ServiceLocator.Instance.GetService<IPlayerStateService>();
            var state = stateService.GetState();
          
            var viewIndex = 0;
            if (state != null)
                viewIndex = state.ballViewIndex;
            
            SetView(globalConfig.ballViews[viewIndex]);
        }
    }
}