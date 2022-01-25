using System;
using UnityEngine;

namespace Inventory_System
{
    public class InventorySlot
    {
        public event Action onItemChange;

        private InventoryItem _item;

        public InventoryItem Item
        {
            get => _item;
            set
            {
                _item = value;
                onItemChange?.Invoke();
            }
        }

        public void Clear()
        {
            _item = null;
            onItemChange?.Invoke();
        }
    }
}
