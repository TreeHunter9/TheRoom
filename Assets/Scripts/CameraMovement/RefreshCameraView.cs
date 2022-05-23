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
            CinemachineCameraHelper.RefreshCamera(_cinemachineDefaultCamera);
        }

        public void Disable() => this.enabled = false;
        public void Enable() => this.enabled = true;
    }
}
