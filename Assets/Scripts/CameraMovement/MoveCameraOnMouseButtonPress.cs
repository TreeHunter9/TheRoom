using System;
using Cinemachine;
using TheRoom.InventorySystem.Core;
using TheRoom.UI;
using UnityEngine;
using Zenject;

namespace TheRoom.CameraMovement
{
    public class MoveCameraOnMouseButtonPress : MonoBehaviour
    {
        [Range(0.001f, 10f)]
        [SerializeField] private float _sensitivity = 1f;
        
        [Inject] private CameraSettings _cameraSettings;

        [Inject] private InventoryCursorChannel _inventoryCursorChannel;

        [Inject] private CinemachineBrain _cinemachineBrain;
        
        private bool _canMove = true;

        private void Awake()
        {
            _sensitivity = _cameraSettings.sensitivity;
        }

        private void Start()
        {
            CinemachineCore.GetInputAxis = GetAxis;
        }

        private void OnEnable()
        {
            MouseClickOnObject.onStartInteractionWithObject += StopMovement;
            MouseClickOnObject.onStopInteractionWithObject += StartMovement;
            MenuController.onMenuEnable += StopMovement;
            MenuController.onMenuDisable += StartMovement;
            _inventoryCursorChannel.onItemSlotHold += item => StopMovement();
            _inventoryCursorChannel.onItemSlotDown += StartMovement;
        }

        private void OnDisable()
        {
            MouseClickOnObject.onStartInteractionWithObject -= StopMovement;
            MouseClickOnObject.onStopInteractionWithObject -= StartMovement;
            MenuController.onMenuEnable -= StopMovement;
            MenuController.onMenuDisable -= StartMovement;
            _inventoryCursorChannel.onItemSlotDown -= StartMovement;
        }

        private float GetAxis(string axisName)
        {
            if (_cinemachineBrain.ActiveBlend != null || _canMove == false)
                return 0;

            return axisName switch
            {
                "Mouse X" when Input.GetMouseButton(0) => Input.GetAxis("Mouse X") * -1f 
                    * _sensitivity,
                "Mouse X" => 0,
                "Mouse Y" when Input.GetMouseButton(0) => Input.GetAxis("Mouse Y") * -1f 
                    * _sensitivity * 2f,
                "Mouse Y" => 0,
                _ => Input.GetAxis(axisName)
            };
        }

        public void StopMovement()
        {
            _canMove = false;
        }

        public void StartMovement() => _canMove = true;

        public void SetSensitivity(float value)
        {
            _sensitivity = value;
            _cameraSettings.sensitivity = value;
        }
    }
}
