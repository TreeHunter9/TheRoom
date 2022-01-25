using System;
using UnityEngine;

namespace Inventory_System
{
    public class InventoryHolder : MonoBehaviour
    {
        [SerializeField] private int _slotsCount;
        [SerializeField] private InventoryChannel _inventoryChannel;

        private Inventory _inventory = new Inventory();
        public Inventory Inventory => _inventory;

        private void Awake()
        {
            _inventoryChannel.onInventoryItemLoot += InventoryItemLoot;
            for (int i = 0; i < _slotsCount; i++)
            {
                _inventory.CreateSlot();
            }
        }

        private void OnDisable()
        {
            _inventoryChannel.onInventoryItemLoot -= InventoryItemLoot;
        }

        private void Update()
        {
            
        }

        private void InventoryItemLoot(InventoryItem item)
        {
            _inventory.FindEmptySlot().Item = item;
        }
    }
}
