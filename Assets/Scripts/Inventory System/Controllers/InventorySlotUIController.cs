using System;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory_System
{
    public class InventorySlotUIController : MonoBehaviour
    {
        private Image _image;

        private InventorySlot _inventorySlot;

        public InventorySlot InventorySlot
        {
            get => _inventorySlot;
            set
            {
                _inventorySlot = value;
                UpdateSlot();
                
                _inventorySlot.onItemChange += UpdateSlot;
            }
        }

        private void Awake()
        {
            _image = GetComponent<Image>();
        }

        private void UpdateSlot()
        {
            _image.sprite = _inventorySlot.Item != null ? _inventorySlot.Item.sprite : null;
        }
    }
}
