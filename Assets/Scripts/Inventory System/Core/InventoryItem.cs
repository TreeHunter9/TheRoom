using UnityEngine;

namespace Inventory_System
{
    [CreateAssetMenu(menuName = "Inventory/InventoryItem")]
    public class InventoryItem : ScriptableObject
    {
        public GameObject gameObject;
        public Sprite sprite;
    }
}
