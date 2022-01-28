using Interactable_object;
using Inventory_System;
using UnityEngine;
using UnityEngine.UI;

namespace CameraMovement
{
    public class CursorController : MonoBehaviour
    {
        private Camera _mainCamera;
        private Image _image;
        
        private InventoryItem _item;
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
            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hit))
            {
                if (hit.transform.CompareTag(Utilities.Constants.NeededItemTag))
                {
                    ItemNeeded itemNeeded = hit.transform.GetComponent<ItemNeeded>();
                    _image.color = itemNeeded.NeededItem == _item ? Color.white : _halfTransparent;
                }
                else
                {
                    _image.color = _halfTransparent;
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                if (hit.distance > 0f && hit.transform.CompareTag(Utilities.Constants.NeededItemTag))
                {
                    ItemNeeded itemNeeded = hit.transform.GetComponent<ItemNeeded>();
                    itemNeeded.SetupItem();
                }
                _cursorHolder.Clear();
            }
        }

        public void StartFindInteraction(InventoryItem item)
        {
            _item = item;
            this.enabled = true;
        }
    }
}
