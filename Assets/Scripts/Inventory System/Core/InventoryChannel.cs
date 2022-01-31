using System;
using UnityEngine;

namespace Inventory_System
{
    [CreateAssetMenu(menuName = "Inventory/Channels/InventoryChannel")]
    public class InventoryChannel : ScriptableObject
    {
        public event Action<InventoryItem> onInventoryItemLoot;

        public void RaiseLootItemEvent(InventoryItem item)
        {
            onInventoryItemLoot?.Invoke(item);
        }
    }
}
