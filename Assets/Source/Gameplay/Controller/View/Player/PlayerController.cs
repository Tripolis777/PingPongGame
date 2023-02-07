using Source.Configs;
using Source.View;
using UnityEngine;

namespace Source.Gameplay.Player
{
    public sealed class PlayerController : GameplayViewController<PlayerViewComponent>
    {
        private IPlayerInput _playerInput;
        private Vector3 _velocity;
        private float _lastTimeUpdate;
        private PlayerConfig _playerConfig; 

        private Vector3 velocity
        {
            get
            {
                var time = Time.time;
                _velocity = Vector3.Lerp(_velocity, Vector3.zero,
                    (time - _lastTimeUpdate) / _playerConfig.velocityFadeTime);
                
                return _velocity;
            }
            set => _velocity = value;
        }
        
        public PlayerController(PlayerViewComponent view, PlayerConfig config) : base(view)
        {
            _playerConfig = config;
            _playerInput = PlayerInputFactory.CreatePlayerInput(config.inputSettings, view);
        }
        
        public override void Init()
        {
            base.Init();
            _playerInput.OnChangePosition += OnChangePosition;
        }

        protected override void OnPause()
        {
            base.OnPause();
            _playerInput.Pause();
        }

        protected override void OnResume()
        {
            base.OnResume();
            _playerInput.Resume();
        }

        private void OnChangePosition(Vector3 delta)
        {
            var viewDelta = View.UpdatePosition(delta);
            _velocity += viewDelta;
            _lastTimeUpdate = Time.time;
        }

        protected override void DisposeInternal()
        {
            base.DisposeInternal();
            _playerInput.Dispose();
            _playerInput = null;
        }
    }
}