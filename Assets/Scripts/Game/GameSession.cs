using Raton.Control;
using Raton.Items;
using UnityEngine;

namespace Raton.Game
{
    internal sealed class GameSession : MonoBehaviour
    {
        private ItemCollector mouseCollector;

        private void Awake() 
        {
            mouseCollector = FindObjectOfType<MouseController>().GetComponent<ItemCollector>();
        }

        private void OnEnable() 
        {
            mouseCollector.AllItemsCollected += StopGame;
        }

        private void OnDisable() 
        {
            mouseCollector.AllItemsCollected -= StopGame;
        }

        private void StopGame() => Application.Quit(exitCode: 0);
    }
}
