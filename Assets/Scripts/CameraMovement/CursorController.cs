using System;
using TheRoom.InteractableObjects;
using TheRoom.InventorySystem.Core;
using TheRoom.Utilities;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace TheRoom.CameraMovement
{
    public class CursorController : MonoBehaviour
    {
        [Inject] private InventoryCursorChannel _inventoryCursorChannel;
        
        private Camera _mainCamera;
        private Image _image;
        
        private InventoryItem _inventoryItem;

        private Color _halfTransparent = new Color(1f, 1f, 1f, 0.5f);
        private bool _itemIsFit;

        private void Awake()
        {
            _inventoryCursorChannel.onItemSlotHold += StartFindInteraction;
            _inventoryCursorChannel.onItemSlotDown += SetupItem;
            _mainCamera = Camera.main;
            _image = GetComponent<Image>();
            this.enabled = false;
        }

        private void OnDestroy()
        {
            _inventoryCursorChannel.onItemSlotHold -= StartFindInteraction;
            _inventoryCursorChannel.onItemSlotDown -= SetupItem;
        }

        private void Update()
        {
            print(_image.color.a);
            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hit))
            {
                if (hit.transform.CompareTag(Constants.NeededItemTag))
                {
                    ItemNeeded itemNeeded = hit.transform.GetComponent<ItemNeeded>();
                    _itemIsFit = itemNeeded.NeededItemType == _inventoryItem.ItemType;
                    _image.color = _itemIsFit ? Color.white : _halfTransparent;
                    return;
                }
            }
            _image.color = _halfTransparent;
        }

        private void SetupItem()
        {
            if (_itemIsFit == false)
            {
                this.enabled = false;
                return;
            }
            
            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out var hit);
            
            ItemNeeded itemNeeded = hit.transform.GetComponent<ItemNeeded>();
            if (itemNeeded.IsSimpleItem())
            {
                itemNeeded.SetupItem();
            }
            else
            {
                StateOfInventoryItem stateOfInventoryItem =
                    _inventoryItem.ItemGO.GetComponent<StateOfInventoryItem>();
                if (stateOfInventoryItem.Compare(itemNeeded.GetPositionsPair()) == true)
                {
                    itemNeeded.SetupItem();
                }
            }

            this.enabled = false;
        }

        public void StartFindInteraction(InventoryItem itemType)
        {
            _inventoryItem = itemType;
            this.enabled = true;
        }
    }
}
