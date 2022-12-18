using System.Linq;
using UnityEngine;

namespace Raton.Items
{
    internal sealed class RandomSpawner : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("The prefab of the item to spawn.")]
        private GameObject itemPrefab;
        [SerializeField]
        [Tooltip("The minimum number of items to spawn.")]
        [Min(0)]
        private int minimumItemsToSpawn;
        [SerializeField]
        [Tooltip("The maximum number of items to spawn.")]
        [Min(1)]
        private int maximumItemsToSpawn;
        [SerializeField]
        [Tooltip("The minimum coordinates at which the item can be spawned.")]
        private Vector2 minimumBounds;
        [SerializeField]
        [Tooltip("The maximum coordinates at which the item can be spawned.")]
        private Vector2 maximumBounds;
        [SerializeField]
        [Tooltip("Tags of the game objects to avoid overlap with.")]
        private string[] cannotOverlap;

        private void Awake() 
        {
            Debug.Assert(minimumBounds.x <= maximumBounds.x, "Minimum horizontal bound should be less than or equal to maximum horizontal bound.");
            Debug.Assert(minimumBounds.y <= maximumBounds.y, "Minimum vertical bound should be less than or equal to maximum vertical bound.");

            float itemRadius = GetItemRadius();
            int itemsToSpawn = Random.Range(minimumItemsToSpawn, maximumItemsToSpawn + 1);
            for (int i = 0; i < itemsToSpawn; i++)
            {
                Instantiate(
                    itemPrefab, 
                    GetRandomPosition(itemRadius), 
                    itemPrefab.transform.rotation,
                    transform
                );
            }
        }

        /// <summary>
        /// Computes the radius of the item for overlap detection.
        /// </summary>
        /// <returns>The radius of the item's collision volume.</returns>
        private float GetItemRadius()
        {
            var collider = itemPrefab.GetComponent<Collider2D>();
            Vector3? extents = collider is null ? null : collider.bounds.extents;
            if (extents is null || cannotOverlap.Length == 0)
            {
                return 0;
            }

            return Mathf.Max(extents.Value.x, extents.Value.y);
        }

        /// <summary>
        /// Computes a random position (avoiding overlap if applicable) at which
        /// the game object may be spawned.
        /// </summary>
        /// <param name="itemRadius">The radius of the item's collision volume.</param>
        /// <returns>The spawning position.</returns>
        private Vector2 GetRandomPosition(float itemRadius)
        {
            Vector2 position;

            do
            {
                position = new Vector2(
                    Random.Range(minimumBounds.x, maximumBounds.x),
                    Random.Range(minimumBounds.y, maximumBounds.y)
                );
            } while (IsPositionOccupied());
            
            return position;

            bool IsPositionOccupied() => Physics2D.OverlapCircleAll(position, itemRadius).Any(collider => 
                cannotOverlap.Any(tag => collider.CompareTag(tag))
            );
        }
    }
}
