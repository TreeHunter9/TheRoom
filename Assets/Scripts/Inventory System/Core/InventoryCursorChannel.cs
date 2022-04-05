using System;
using UnityEngine;

namespace TheRoom.InventorySystem.Core
{
    [CreateAssetMenu(menuName = "Inventory/Channels/InventoryCursorChannel")]
    public class InventoryCursorChannel : ScriptableObject
    {
        public event Action<InventoryItem> onItemSlotHold;
        public event Action<InventoryItem> onItemSlotClick;
        public event Action onItemSlotDown;

        public void RaiseItemSlotHoldEvent(InventoryItem item) => onItemSlotHold?.Invoke(item);

        public void RaiseItemSlotClickEvent(InventoryItem item) => onItemSlotClick?.Invoke(item);

        public void RaiseItemSlotDownEvent() => onItemSlotDown?.Invoke();
    }
}
