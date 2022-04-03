using UnityEngine;

namespace TheRoom.InventorySystem.Core
{
    [CreateAssetMenu(menuName = "Inventory/InventoryItem")]
    public class InventoryItemType : ScriptableObject
    {
        public GameObject gameObjectForSpawn;
        public GameObject gameObjectForInteraction;
        public Sprite sprite;
    }
}
