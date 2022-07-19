using System;
using UnityEngine;

namespace Raton.Items
{
    [RequireComponent(typeof(Rigidbody2D))]
    internal sealed class ItemCollector : MonoBehaviour
    {
        /// <summary>
        /// Event fired when all items with the given tag have been collected.
        /// </summary>
        internal event Action AllItemsCollected;

        [SerializeField]
        [Tooltip("The tag of the items to be collected.")]
        private string itemTag;

        private int totalItems;
        private int itemsCollected;

        private void Awake() 
        {
            totalItems = GameObject.FindGameObjectsWithTag(itemTag).Length;
        }
        
        private void OnTriggerEnter2D(Collider2D other) 
        {
            if (!other.CompareTag(itemTag))
            {
                return;
            }

            Destroy(other.gameObject);

            if (++itemsCollected == totalItems)
            {
                AllItemsCollected?.Invoke();
            }
        }
    }
}
