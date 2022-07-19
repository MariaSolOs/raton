using UnityEngine;
using UnityEngine.InputSystem;
using Raton.Movement;
using Raton.Items;

namespace Raton.Control 
{
    [RequireComponent(typeof(Mover))]
    [RequireComponent(typeof(ItemCollector))]
    internal sealed class MouseController : MonoBehaviour
    {
        private Mover mover;
        private ItemCollector itemCollector;
        private Vector2 moveDirection;
        private bool hasCollectedAllCheese;

        private void Awake() 
        {
            mover = GetComponent<Mover>();
            itemCollector = GetComponent<ItemCollector>();
        }

        private void OnEnable() 
        {
            itemCollector.AllItemsCollected += OnAllItemsCollected;
        }

        private void OnDisable() 
        {
            itemCollector.AllItemsCollected -= OnAllItemsCollected;
        }

        private void FixedUpdate() => mover.Move(moveDirection);

        private void OnTriggerEnter2D(Collider2D other) 
        {
            if (other.CompareTag("Mouse Hole"))
            {
                if (hasCollectedAllCheese)
                {
                    // WIN
                }
                else
                {
                    // Hide
                }
            }
        }

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

        private void OnAllItemsCollected()
        {
            hasCollectedAllCheese = true;
        }
    }
}
