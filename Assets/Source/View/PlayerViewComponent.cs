using Source.Gameplay;
using Source.Gameplay.Player;
using UnityEngine;

namespace Source.View
{
    public sealed class PlayerViewComponent : ViewComponent<PlayerController>, IBallInteractable
    {
        [SerializeField] private GameObject playerView;
        [SerializeField] private Collider playerCollider;
        [SerializeField] private Collider[] walls;
        
        private Vector2 xBorders;

        public override void Init(GameplayController controller)
        {
            base.Init(controller);

            var playerWidth = playerCollider.bounds.size.x / 2;
            xBorders = new Vector2(float.MaxValue, float.MinValue);
            foreach (var wall in walls)
            {
                var wallBounds = wall.bounds;
                var width = wallBounds.size.x;
                var minX = wallBounds.center.x - width / 2;
                var maxX = wallBounds.center.x + width / 2;

                xBorders.x = Mathf.Min(xBorders.x, maxX);
                xBorders.y = Mathf.Max(xBorders.y, minX);
            }

            xBorders.x += playerWidth;
            xBorders.y -= playerWidth;
        }

        public Vector3 UpdatePosition(Vector3 deltaPosition)
        {
            var lastPosition = transform.position;
            var newPosition = lastPosition + deltaPosition;

            // Check walls
            if (newPosition.x < xBorders.x)
                newPosition.x = xBorders.x;
            else if (newPosition.x > xBorders.y)
                newPosition.x = xBorders.y;
            
            transform.position = newPosition;
            return newPosition;
        }

        // private IEnumerator FadeVelocity()
        // {
        //     var fadeTime = 0f;
        //     var startVelocity = velocity;
        //     while (fadeTime < velocityFadeTime)
        //     {
        //         velocity = Vector3.Lerp(startVelocity, Vector3.zero, fadeTime / velocityFadeTime);
        //         yield return null;
        //     }
        //     velocity = Vector3.zero;
        // }

        public Vector3 GetForce() => Vector3.zero;
    }
}