using Cinemachine;
using UnityEngine;

namespace TheRoom.CameraMovement
{
    public class RefreshCameraView : MonoBehaviour
    {
        [SerializeField] private CinemachineFreeLook _cinemachineDefaultCamera;

        private CinemachineBrain _cinemachineBrain;

        private void Awake()
        {
            _cinemachineBrain = GetComponent<CinemachineBrain>();
            
            MouseClickOnObject.onStartInteractionWithObject += Disable;
            MouseClickOnObject.onStopInteractionWithObject += Enable;
            CursorHolder.onStartDragItem += Disable;
            CursorHolder.onStopDragItem += Enable;
        }

        private void OnDestroy()
        {
            MouseClickOnObject.onStartInteractionWithObject -= Disable;
            MouseClickOnObject.onStopInteractionWithObject -= Enable;
            CursorHolder.onStartDragItem -= Disable;
            CursorHolder.onStopDragItem -= Enable;
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
            CameraChanger.ChangeCamera(_cinemachineDefaultCamera);
        }

        private void Disable() => this.enabled = false;
        private void Enable() => this.enabled = true;
    }
}
