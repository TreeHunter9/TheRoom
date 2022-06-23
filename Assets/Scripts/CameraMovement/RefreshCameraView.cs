using System;
using Cinemachine;
using TheRoom.InventorySystem.Core;
using UnityEngine;
using Zenject;

namespace TheRoom.CameraMovement
{
    public class RefreshCameraView : MonoBehaviour
    {
        [SerializeField] private CinemachineFreeLook _cinemachineDefaultCamera;

        [Inject] private InventoryCursorChannel _inventoryCursorChannel;

        [Inject] private CinemachineBrain _cinemachineBrain;

        private bool _isBlocked = false;
        private Action<InventoryItem> _disableEventHandlerWithItem;

        private void Awake()
        {
            _disableEventHandlerWithItem = item => Disable();
            
            MouseClickOnObject.onStartInteractionWithObject += Disable;
            MouseClickOnObject.onStopInteractionWithObject += Enable;
            _inventoryCursorChannel.onItemSlotHold += _disableEventHandlerWithItem;
            _inventoryCursorChannel.onItemSlotDown += Enable;
        }

        private void OnDestroy()
        {
            MouseClickOnObject.onStartInteractionWithObject -= Disable;
            MouseClickOnObject.onStopInteractionWithObject -= Enable;
            _inventoryCursorChannel.onItemSlotHold -= _disableEventHandlerWithItem;
            _inventoryCursorChannel.onItemSlotDown -= Enable;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(1) && _isBlocked == false)
            {
                ChangeView();
            }
        }

        private void ChangeView()
        {
            CinemachineCameraHelper.RefreshCamera(_cinemachineDefaultCamera);
        }

        public void Disable() => this.enabled = false;
        public void Enable() => this.enabled = true;

        public void Block() => _isBlocked = true;
        public void Unblock() => _isBlocked = false;
    }
}
