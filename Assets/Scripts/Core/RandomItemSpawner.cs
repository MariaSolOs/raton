using UnityEngine;
using UnityEngine.Assertions;

namespace Raton.Core
{
    public class RandomItemSpawner : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("The prefab of the item to spawn.")]
        private GameObject itemPrefab;
        [SerializeField]
        [Tooltip("The number of items to spawn.")]
        [Min(1)]
        private int itemsToSpawn;
        [SerializeField]
        [Tooltip("The minimum coordinates at which the item can be spawned.")]
        private Vector2 minimumBounds;
        [SerializeField]
        [Tooltip("The maximum coordinates at which the item can be spawned.")]
        private Vector2 maximumBounds;

        private void Awake() 
        {
            Debug.Assert(minimumBounds.x <= maximumBounds.x, "Minimum horizontal bound should be less than or equal to maximum horizontal bound.");
            Debug.Assert(minimumBounds.y <= maximumBounds.y, "Minimum vertical bound should be less than or equal to maximum vertical bound.");

            for (int i = 0; i < itemsToSpawn; i++)
            {
                Instantiate(
                    itemPrefab, 
                    GetRandomPosition(), 
                    itemPrefab.transform.rotation,
                    transform
                );
            }

            Vector2 GetRandomPosition() => new Vector2(
                Random.Range(minimumBounds.x, maximumBounds.x),
                Random.Range(minimumBounds.y, maximumBounds.y)
            );
        }
    }
}
