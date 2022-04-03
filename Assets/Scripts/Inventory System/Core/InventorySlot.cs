using System;

namespace TheRoom.InventorySystem.Core
{
    public class InventorySlot
    {
        private InventoryItem _inventoryItem;

        public event Action<InventoryItem> onItemChange;

        public InventoryItem Item
        {
            get => _inventoryItem;
            set
            {
                _inventoryItem = value;
                onItemChange?.Invoke(_inventoryItem);
            }
        }

        public void Clear()
        {
            _inventoryItem = null;
            onItemChange?.Invoke(null);
        }
    }
}
