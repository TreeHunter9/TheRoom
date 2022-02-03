using System;

namespace Inventory_System
{
    public class InventorySlot
    {
        private InventoryItem _item;
        private InteractableItem _interactableItem;

        public event Action<InventoryItem> onItemChange;

        public InventoryItem Item
        {
            get => _item;
            set
            {
                _item = value;
                onItemChange?.Invoke(_item);
            }
        }

        public InteractableItem InteractableItemData
        {
            get => _interactableItem;
            set => _interactableItem = value;
        }

        public void Clear()
        {
            _item = null;
            _interactableItem = null;
            onItemChange?.Invoke(_item);
        }

        public void SetData(InventoryItem item, InteractableItem interactableItem)
        {
            Item = item;
            InteractableItemData = interactableItem;
        }
    }
}
