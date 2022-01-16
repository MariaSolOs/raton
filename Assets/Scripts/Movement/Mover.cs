using UnityEngine;

namespace Raton.Movement
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Animator))]
    public class Mover : MonoBehaviour
    {
        private const float IdleEpsilon = 0.01F;

        [SerializeField]
        private float speed;

        private new Rigidbody2D rigidbody;
        private Animator animator;
        private bool movingBackwards;

        private void Awake() 
        {
            rigidbody = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
        }

        public void Move(Vector2 direction)
        {
            rigidbody.velocity = direction * speed;

            // Rotate the sprite when moving to the left
            if (direction.x < 0)
            {
                transform.right = Vector2.left;
            }
            else if (direction.x > 0)
            {
                transform.right = Vector2.right;
            }

            // For distinguishing front and back idle states
            float verticalVelocity = direction.y; 
            if (!Mathf.Approximately(direction.y, 0))
            {
                movingBackwards = direction.y > 0;
            }
            else if (movingBackwards)
            {
                verticalVelocity = IdleEpsilon;
            }

            animator.SetFloat("horizontalSpeed", Mathf.Abs(direction.x));
            animator.SetFloat("verticalSpeed", Mathf.Abs(direction.y));
            animator.SetFloat("verticalVelocity", verticalVelocity);
        }
    }
}
