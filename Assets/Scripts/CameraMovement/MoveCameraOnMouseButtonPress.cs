using Cinemachine;
using UI;
using UnityEngine;

namespace CameraMovement
{
    public class MoveCameraOnMouseButtonPress : MonoBehaviour
    {
        [Range(0.1f, 10f)]
        [SerializeField] private float _sensitivity = 2f;

        private float _sensitivityAvailable = 1f;
        private CinemachineBrain _cinemachineBrain;

        private void Awake()
        {
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
            CursorHolder.onStartDragItem += StopMovement;
            CursorHolder.onStopDragItem += StartMovement;
            MenuController.onMenuEnable += StopMovement;
            MenuController.onMenuDisable += StartMovement;
        }

        private void OnDisable()
        {
            MouseClickOnObject.onStartInteractionWithObject -= StopMovement;
            MouseClickOnObject.onStopInteractionWithObject -= StartMovement;
            CursorHolder.onStartDragItem -= StopMovement;
            CursorHolder.onStopDragItem -= StartMovement;
            MenuController.onMenuEnable -= StopMovement;
            MenuController.onMenuDisable -= StartMovement;
        }

        private float GetAxis(string axisName)
        {
            if (_cinemachineBrain.ActiveBlend != null)
                return 0;
            
            return axisName switch
            {
                "Mouse X" when Input.GetMouseButton(0) => Input.GetAxis("Mouse X") * -1f 
                    * _sensitivity * _sensitivityAvailable,
                "Mouse X" => 0,
                "Mouse Y" when Input.GetMouseButton(0) => Input.GetAxis("Mouse Y") * -1f 
                    * _sensitivity * _sensitivityAvailable,
                "Mouse Y" => 0,
                _ => UnityEngine.Input.GetAxis(axisName)
            };
        }

        private void StopMovement() => _sensitivityAvailable = 0f;
        private void StartMovement() => _sensitivityAvailable = 1f;

        public void SetSensitivity(float value)
        {
            _sensitivity = value;
        }
    }
}
