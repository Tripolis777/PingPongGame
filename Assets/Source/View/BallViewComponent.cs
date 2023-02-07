using Source.View;
using UnityEngine;

namespace Source.Gameplay
{
    [RequireComponent(typeof(Collider))]
    public class BallViewComponent : ViewComponent<BallController>
    {
        [SerializeField] private GameObject viewContainer;

        private Vector3 _direction;
        private float _speed;
        
        public void LoadView(GameObject view)
        {
            Instantiate(view, viewContainer.transform);
        }

        public void SetSpeed(float speed) => this._speed = speed;
        
        public void SetDirection(Vector3 direction) => this._direction = direction.normalized;

        private void OnCollisionEnter(Collision collision)
        {
            controller.OnCollision(_direction, collision);
        }

        private void Update()
        {
            var positionDelta = _direction * _speed * Time.deltaTime;
            transform.position += positionDelta;
        }
    }
}