using System;
using System.Collections.Generic;

namespace TheRoom.InventorySystem.Core
{
    public class Inventory
    {
        private readonly List<InventorySlot> _slots = new List<InventorySlot>();

        public void CreateSlot()
        {
            InventorySlot slot = new InventorySlot();
            _slots.Add(slot);
        }

        public void Clear()
        {
            _slots.ForEach(slot => slot.Clear());
        }

        public InventorySlot FindEmptySlot()
        {
            return _slots.Find(slot => slot.Item == null);
        }

        public void ForEach(Action<InventorySlot> action)
        {
            _slots.ForEach(action);
        }
    }
}
