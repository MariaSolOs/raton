using UnityEngine;

namespace Raton.Items
{
    [RequireComponent(typeof(Rigidbody2D))]
    internal sealed class ItemCollector : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("The tag of the items to be collected.")]
        private string itemTag;

        private int totalItems;
        private int itemsCollected;

        private void OnTriggerEnter2D(Collider2D other) 
        {
            if (other.CompareTag(itemTag))
            {
                Destroy(other.gameObject);
                itemsCollected++;
            }
        }
    }
}
