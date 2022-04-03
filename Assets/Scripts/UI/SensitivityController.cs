using TheRoom.CameraMovement;
using UnityEngine;
using UnityEngine.UI;

namespace TheRoom.UI
{
    public class SensitivityController : MonoBehaviour
    {
        [SerializeField] private MoveCameraOnMouseButtonPress _moveCameraScript;
    
        private Slider _slider;

        private void Awake()
        {
            _slider = GetComponent<Slider>();
            _slider.onValueChanged.AddListener(_moveCameraScript.SetSensitivity);
        }
    }
}
