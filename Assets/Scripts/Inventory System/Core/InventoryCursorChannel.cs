using System;
using UnityEngine;

namespace Inventory_System
{
    [CreateAssetMenu(menuName = "Inventory/Channels/InventoryCursorChannel")]
    public class InventoryCursorChannel : ScriptableObject
    {
        public event Action<InventoryItem> onItemSlotHold;
        public event Action<InteractableItem> onItemSlotClick;

        public void RaiseItemSlotHoldEvent(InventoryItem item)
        {
            onItemSlotHold?.Invoke(item);
        }

        public void RaiseItemSlotClickEvent(InteractableItem item)
        {
            onItemSlotClick?.Invoke(item);
        }
    }
}
