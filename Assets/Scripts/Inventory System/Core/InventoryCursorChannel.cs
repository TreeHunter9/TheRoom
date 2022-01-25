using System;
using UnityEngine;

namespace Inventory_System
{
    [CreateAssetMenu(menuName = "Inventory/InventoryCursorChannel")]
    public class InventoryCursorChannel : ScriptableObject
    {
        public event Action<InventoryItem> onItemSlotHold;
        public event Action<InventoryItem> onItemSlotClick;

        public void RaiseItemSlotHold(InventoryItem item)
        {
            onItemSlotHold?.Invoke(item);
        }

        public void RaiseItemSlotClick(InventoryItem item)
        {
            onItemSlotClick?.Invoke(item);
        }
    }
}
