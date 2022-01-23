using System;
using UnityEngine;

namespace Inventory_System
{
    public class InventoryUIController : MonoBehaviour
    {
        [SerializeField] private InventorySlotUIController _slotControllerPrefab;
        [SerializeField] private InventoryHolder _inventoryHolder;

        private Inventory _inventory;

        private void Awake()
        {
            _inventory = _inventoryHolder.Inventory;
        }

        private void Start()
        {
            CreateUIInventory();
        }

        private void CreateUIInventory()
        {
            _inventory.onSlotAdded += CreateSlotController;
            _inventory.ForEach(CreateSlotController);
        }

        private void CreateSlotController(InventorySlot slot)
        {
            InventorySlotUIController slotUIController = Instantiate(_slotControllerPrefab, transform);
            slotUIController.InventorySlot = slot;
        }
    }
}
