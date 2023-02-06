using Source.View;
using UnityEngine;

namespace Source.Gameplay
{
    [RequireComponent(typeof(Collider))]
    public class BallViewComponent : ViewComponent<BallController>
    {
        [SerializeField]
        private GameObject viewContainer;

        private Vector3 direction;
        private float speed;
        
        public void LoadView(GameObject view)
        {
            Instantiate(view, viewContainer.transform);
        }

        public void SetSpeed(float speed) => this.speed = speed;
        
        public void SetDirection(Vector3 direction) => this.direction = direction.normalized;

        private void OnCollisionEnter(Collision collision)
        {
            controller.OnCollision(direction, collision);
        }

        private void Update()
        {
            var positionDelta = direction * speed * Time.deltaTime;
            transform.position += positionDelta;
        }
    }
}