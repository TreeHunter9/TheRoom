using System;
using Inventory_System;
using UnityEngine;

namespace CameraMovement
{
    [RequireComponent(typeof(InventorySlotUIController), typeof(CursorController))]
    public class CursorHolder : MonoBehaviour
    {
        [SerializeField] private InventoryCursorChannel _inventoryCursorChannel;
        
        private InventorySlotUIController _cursorSlotUIController;
        private InventorySlot _cursorSlot = new InventorySlot();
        
        private CursorController _cursorController;

        public static event Action onStartDragItem;
        public static event Action onStopDragItem;

        private void Awake()
        {
            _cursorSlotUIController = GetComponent<InventorySlotUIController>();
            _inventoryCursorChannel.onItemSlotHold += ChangeCursorSlotItem;
            _cursorController = GetComponent<CursorController>();
        }

        private void Start()
        {
            _cursorSlotUIController.InventorySlot = _cursorSlot;
        }

        private void ChangeCursorSlotItem(InventoryItem item)
        {
            if (item != null)
            {
                _cursorSlot.Item = item;
                _cursorController.StartFindInteraction(item);
                onStartDragItem?.Invoke();
            }
        }

        public void Clear()
        {
            _cursorSlot.Item = null;
            _cursorController.enabled = false;
            onStopDragItem?.Invoke();
        }
    }
}
