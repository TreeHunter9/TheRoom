using TheRoom.CameraMovement;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace TheRoom.UI
{
    public class SensitivityController : MonoBehaviour
    {
        [SerializeField] private MoveCameraOnMouseButtonPress _moveCameraScript;
        
        [Inject] private CameraSettings _cameraSettings;
    
        private Slider _slider;

        private void Awake()
        {
            _slider = GetComponent<Slider>();
            _slider.onValueChanged.AddListener(_moveCameraScript.SetSensitivity);
            _slider.value = _cameraSettings.sensitivity;
        }
    }
}
