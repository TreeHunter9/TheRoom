using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Inventory_System
{
    [RequireComponent(typeof(InventorySlotUIController))]
    public class ClickableSlot : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private InventoryCursorChannel _inventoryCursorChannel;
        
        private InventorySlotUIController _slotUIController;

        private void Awake()
        {
            _slotUIController = GetComponent<InventorySlotUIController>();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (_slotUIController.InventorySlot.Item != null)
                _inventoryCursorChannel.RaiseItemSlotHoldEvent(_slotUIController.InventorySlot.Item);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (eventData.dragging == false)
                _inventoryCursorChannel.RaiseItemSlotClickEvent(_slotUIController.InventorySlot.InteractableItemData);
        }
    }
}
