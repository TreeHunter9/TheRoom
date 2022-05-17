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
                if (_inventoryItem != null) 
                    _inventoryItem.onNumberOfUsesIsZero += Clear;
                onItemChange?.Invoke(_inventoryItem);
            }
        }

        public void Clear()
        {
            if (_inventoryItem == null)
                return;
            _inventoryItem.onNumberOfUsesIsZero -= Clear;
            _inventoryItem = null;
            onItemChange?.Invoke(null);
        }
    }
}
