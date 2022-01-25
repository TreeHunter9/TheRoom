using UnityEngine;
using UnityEngine.UI;

namespace Inventory_System
{
    public class InventorySlotUIController : MonoBehaviour
    {
        [SerializeField] private Color _spriteColor = Color.white;
        
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
            if (_inventorySlot.Item != null)
            {
                _image.sprite = _inventorySlot.Item.sprite;
                _image.color = _spriteColor;
            }
            else
            {
                _image.sprite = null;
                _image.color = Color.clear;
            }
        }
    }
}
