using System;
using UnityEngine;

namespace TheRoom.InventorySystem.Core
{
    [CreateAssetMenu(menuName = "Inventory/Channels/InventoryChannel")]
    public class InventoryChannel : ScriptableObject
    {
        public event Action<InventoryItemType, GameObject> onInventoryItemLoot;

        public void RaiseLootItemEvent(InventoryItemType itemType, GameObject itemGO)
        {
            onInventoryItemLoot?.Invoke(itemType, itemGO);
        }
    }
}
