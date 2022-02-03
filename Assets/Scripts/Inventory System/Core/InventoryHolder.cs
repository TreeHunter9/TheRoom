using System;
using Cinemachine;
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

        private void InventoryItemLoot(InventoryItem item, GameObject itemGO)
        {
            InventorySlot slot = _inventory.FindEmptySlot();
            slot.SetData(item, new InteractableItem(
                itemGO, itemGO.GetComponentInChildren<CinemachineFreeLook>()));
        }
    }
}
