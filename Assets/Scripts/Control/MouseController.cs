using UnityEngine;
using Raton.Movement;

namespace Raton.Control 
{
    [RequireComponent(typeof(Mover))]
    public class MouseController : MonoBehaviour
    {
        private Mover mover;
        private Vector2 moveDirection;

        private void Awake() 
        {
            mover = GetComponent<Mover>();
        }

        private void FixedUpdate() 
        {
            mover.Move(moveDirection);
        }

        private void Update() 
        {
            DirectionFromInput();
        }

        private void DirectionFromInput()
        {
            moveDirection.x = Input.GetAxisRaw("Horizontal");
            moveDirection.y = Input.GetAxisRaw("Vertical");

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
