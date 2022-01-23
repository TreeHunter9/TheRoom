using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Inventory_System
{
    public class ClickOnSlot : MonoBehaviour, IPointerDownHandler
    {
        private InventorySlotUIController _inventorySlotUIController;

        private void Awake()
        {
            _inventorySlotUIController = GetComponent<InventorySlotUIController>();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            print(_inventorySlotUIController.InventorySlot.Item);
            //TODO: Сделать просмотр объекта
        }
    }
}
