using TheRoom.InteractableObjects;
using TheRoom.InventorySystem.Core;
using TheRoom.Utilities;
using UnityEngine;
using UnityEngine.UI;

namespace TheRoom.CameraMovement
{
    public class CursorController : MonoBehaviour
    {
        private Camera _mainCamera;
        private Image _image;
        
        private InventoryItem _inventoryItem;
        private CursorHolder _cursorHolder;

        private Color _halfTransparent = new Color(1f, 1f, 1f, 0.5f);

        private void Awake()
        {
            _mainCamera = Camera.main;
            _image = GetComponent<Image>();
            _cursorHolder = GetComponent<CursorHolder>();
            this.enabled = false;
        }

        private void Update()
        {
            bool itemIsFit = false;
            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hit))
            {
                if (hit.transform.CompareTag(Constants.NeededItemTag))
                {
                    ItemNeeded itemNeeded = hit.transform.GetComponent<ItemNeeded>();
                    itemIsFit = itemNeeded.NeededItemType == _inventoryItem.ItemType;
                    _image.color = itemIsFit ? Color.white : _halfTransparent;
                }
                else
                {
                    _image.color = _halfTransparent;
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                if (itemIsFit != true)
                {
                    _cursorHolder.Clear();
                    return;
                }

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

                _cursorHolder.Clear();
            }
        }

        public void StartFindInteraction(InventoryItem itemType)
        {
            _inventoryItem = itemType;
            this.enabled = true;
        }
    }
}
