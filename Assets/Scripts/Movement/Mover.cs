using UnityEngine;

namespace Raton.Movement
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Animator))]
    internal sealed class Mover : MonoBehaviour
    {
        private const float IdleEpsilon = 0.01F;

        [SerializeField]
        private float speed;

        private Rigidbody2D rigidBody;
        private Animator animator;
        private bool movingBackwards;

        private void Awake() 
        {
            rigidBody = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
        }

        public void Move(Vector2 direction)
        {
            direction.Normalize();
            rigidBody.velocity = direction * speed;

            // Rotate the sprite when moving to the left
            bool hasHorizontalSpeed = Mathf.Abs(rigidBody.velocity.x) > 0;
            if (hasHorizontalSpeed)
            {
                transform.localScale = new Vector2(Mathf.Sign(rigidBody.velocity.x), 1);
            }

            // For distinguishing front and back idle states
            float verticalDirection = direction.y;
            if (!Mathf.Approximately(verticalDirection, 0))
            {
                movingBackwards = verticalDirection > 0;
            }
            else if (movingBackwards)
            {
                verticalDirection = IdleEpsilon;
            }

            animator.SetFloat("horizontalSpeed", Mathf.Abs(direction.x));
            animator.SetFloat("verticalSpeed", Mathf.Abs(direction.y));
            animator.SetFloat("verticalVelocity", verticalDirection);
        }
    }
}
