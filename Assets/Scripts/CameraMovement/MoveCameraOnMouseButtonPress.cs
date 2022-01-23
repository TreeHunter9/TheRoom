using System;
using Cinemachine;
using UnityEngine;

namespace CameraMovement
{
    public class MoveCameraOnMouseButtonPress : MonoBehaviour
    {
        [Range(0.1f, 10f)]
        [SerializeField] private float _sensitivity = 2f;

        private float _currentSensitivity;
        private CinemachineBrain _cinemachineBrain;
        

        private void Awake()
        {
            _currentSensitivity = _sensitivity;
            _cinemachineBrain = GetComponent<CinemachineBrain>();
        }

        private void Start()
        {
            CinemachineCore.GetInputAxis = GetAxis;
        }

        private void OnEnable()
        {
            MouseClickOnObject.onStartInteractionWithObject += StopMovement;
            MouseClickOnObject.onStopInteractionWithObject += StartMovement;
        }

        private void OnDisable()
        {
            MouseClickOnObject.onStartInteractionWithObject -= StopMovement;
            MouseClickOnObject.onStopInteractionWithObject -= StartMovement;
        }

        private float GetAxis(string axisName)
        {
            if (_cinemachineBrain.ActiveBlend != null)
                return 0;
            
            return axisName switch
            {
                "Mouse X" when Input.GetMouseButton(0) => Input.GetAxis("Mouse X") * -1f * _currentSensitivity,
                "Mouse X" => 0,
                "Mouse Y" when Input.GetMouseButton(0) => Input.GetAxis("Mouse Y") * -1f * _currentSensitivity,
                "Mouse Y" => 0,
                _ => UnityEngine.Input.GetAxis(axisName)
            };
        }

        private void StopMovement() => _currentSensitivity = 0f;
        private void StartMovement() => _currentSensitivity = _sensitivity;
    }
}
