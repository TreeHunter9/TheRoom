using System;
using UnityEngine;

namespace Inventory_System
{
    [CreateAssetMenu(menuName = "Inventory/Channels/InventoryChannel")]
    public class InventoryChannel : ScriptableObject
    {
        public event Action<InventoryItem, GameObject> onInventoryItemLoot;

        public void RaiseLootItemEvent(InventoryItem item, GameObject itemGO)
        {
            onInventoryItemLoot?.Invoke(item, itemGO);
        }
    }
}
