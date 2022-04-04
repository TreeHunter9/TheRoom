using UnityEngine;

namespace TheRoom.InventorySystem.Core
{
    [CreateAssetMenu(menuName = "Inventory/InventoryItem")]
    public class InventoryItemType : ScriptableObject
    {
        public GameObject prefabForInteraction;
        public GameObject prefabForLook;
        public Sprite sprite;
    }
}
