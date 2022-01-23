using System;
using UnityEngine;

namespace Inventory_System
{
    [CreateAssetMenu(menuName = "Inventory/InventoryChannel")]
    public class InventoryChannel : ScriptableObject
    {
        public event Action<InventoryItem> onInventoryItemLoot;

        public void RaiseLootItem(InventoryItem item)
        {
            onInventoryItemLoot?.Invoke(item);
        }
    }
}
