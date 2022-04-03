using TheRoom.InventorySystem.Core;
using UnityEngine;
using UnityEngine.UI;

namespace TheRoom.InventorySystem.Controllers
{
    public class InventorySlotUIController : MonoBehaviour
    {
        [SerializeField] private Color _spriteColor = Color.white;
        
        private Image _image;

        private InventorySlot _inventorySlot;
        private GameObject _invenoryItemGO;

        public InventorySlot InventorySlot
        {
            get => _inventorySlot;
            set
            {
                _inventorySlot = value;
                UpdateSlot(_inventorySlot.Item);
                
                _inventorySlot.onItemChange += UpdateSlot;
            }
        }

        private void Awake()
        {
            _image = GetComponent<Image>();
        }

        private void UpdateSlot(InventoryItem item)
        {
            if (item != null)
            {
                _image.sprite = _inventorySlot.Item.Sprite;
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
