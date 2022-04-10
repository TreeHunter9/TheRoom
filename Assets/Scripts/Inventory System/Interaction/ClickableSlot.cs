using TheRoom.InventorySystem.Controllers;
using TheRoom.InventorySystem.Core;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace TheRoom.InventorySystem.Interaction
{
    [RequireComponent(typeof(InventorySlotUIController))]
    public class ClickableSlot : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
    {
        [Inject] private InventoryCursorChannel _inventoryCursorChannel;
        
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
                _inventoryCursorChannel.RaiseItemSlotClickEvent(_slotUIController.InventorySlot.Item);
            _inventoryCursorChannel.RaiseItemSlotDownEvent();
        }

        public void OnDrag(PointerEventData eventData) { }
    }
}
