using System;
using TheRoom.InventorySystem.Controllers;
using TheRoom.InventorySystem.Core;
using UnityEngine;
using Zenject;

namespace TheRoom.CameraMovement
{
    [RequireComponent(typeof(InventorySlotUIController), typeof(CursorController))]
    public class CursorSlotUIController : InventorySlotUIController
    {
        [Inject] private InventoryCursorChannel _inventoryCursorChannel;

        private void Start()
        {
            InventorySlot = new InventorySlot();

            _inventoryCursorChannel.onItemSlotHold += SetSlotItem;
            _inventoryCursorChannel.onItemSlotDown += ClearSlotItem;
        }

        private void OnDestroy()
        {
            _inventoryCursorChannel.onItemSlotHold -= SetSlotItem;
            _inventoryCursorChannel.onItemSlotDown -= ClearSlotItem;
        }

        private void SetSlotItem(InventoryItem item) => InventorySlot.Item = item;
        private void ClearSlotItem() => InventorySlot.Clear();
    }
}
