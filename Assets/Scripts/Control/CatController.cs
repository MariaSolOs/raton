using UnityEngine;
using Raton.Movement;

namespace Raton.Control
{
    [RequireComponent(typeof(Mover))]
    internal sealed class CatController : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("The distance at which the cat will stop chasing the mouse.")]
        private float chaseDistance;

        private Mover mover;
        private Transform mouse;

        private void Awake() 
        {
            mover = GetComponent<Mover>();
            mouse = FindObjectOfType<MouseController>().transform;
        }

        private void FixedUpdate() => Move();

        private void Move()
        {
            // Stop moving when we're close enough
            if (Vector2.Distance(mouse.position, transform.position) <= chaseDistance)
            {
                mover.Move(Vector2.zero);
                return;
            }

            Vector2 direction = Vector2.zero;
            float deltaX = mouse.position.x - transform.position.x;
            float deltaY = mouse.position.y - transform.position.y;

            // Chase on the horizontal axis before chasing vertically
            if (Mathf.Abs(deltaX) >= chaseDistance)
            {
                direction.x = Mathf.Sign(deltaX);
            }
            else if (Mathf.Abs(deltaY) >= chaseDistance)
            {
                direction.y = Mathf.Sign(deltaY);
            }

            mover.Move(direction);
        }
    }
}
