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

        private void Awake()
        {
            MouseClickOnObject.onStartInteractionWithObject += Disable;
            MouseClickOnObject.onStopInteractionWithObject += Enable;
            _inventoryCursorChannel.onItemSlotHold += item => Disable();
            _inventoryCursorChannel.onItemSlotDown += Enable;
        }

        private void OnDestroy()
        {
            MouseClickOnObject.onStartInteractionWithObject -= Disable;
            MouseClickOnObject.onStopInteractionWithObject -= Enable;
            _inventoryCursorChannel.onItemSlotDown -= Enable;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(1))
            {
                ChangeView();
            }
        }

        private void ChangeView()
        {
            if (_cinemachineBrain.ActiveVirtualCamera.Follow.TryGetComponent(out HasCamera hasCamera))
                hasCamera.DisableInteractableObjects();
            CinemachineCameraHelper.RefreshCamera(_cinemachineDefaultCamera);
        }

        private void Disable() => this.enabled = false;
        private void Enable() => this.enabled = true;
    }
}
