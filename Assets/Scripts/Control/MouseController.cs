using UnityEngine;
using UnityEngine.InputSystem;
using Raton.Movement;

namespace Raton.Control 
{
    [RequireComponent(typeof(Mover))]
    internal sealed class MouseController : MonoBehaviour
    {
        private Mover mover;
        private Vector2 moveDirection;

        private void Awake() 
        {
            mover = GetComponent<Mover>();
        }

        private void FixedUpdate() => mover.Move(moveDirection);

        private void OnMove(InputValue value)
        {
            moveDirection = value.Get<Vector2>();

            // Disable diagonal movement
            if (Mathf.Abs(moveDirection.x) > Mathf.Abs(moveDirection.y))
            {
                moveDirection.y = 0; 
            }
            else 
            {
                moveDirection.x = 0;
            }
        }
    }
}
