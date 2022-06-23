using System.Collections;
using Cinemachine;
using TheRoom.CameraMovement;
using UnityEngine;

namespace TheRoom.UI
{
    public class StartMenuController : MonoBehaviour
    {
        [SerializeField] private CinemachineFreeLook _defaultCamera;
        [SerializeField] private MouseClickOnObject _mouseClickOnObjectScript;
        [SerializeField] private MoveCameraOnMouseButtonPress _moveCameraOnMouseButtonPressScript;
        [SerializeField] private RefreshCameraView _refreshCameraViewScript;

        private void Awake()
        {
            _mouseClickOnObjectScript.enabled = false;
            _moveCameraOnMouseButtonPressScript.StopMovement();
            _refreshCameraViewScript.Block();
        }

        public void ExitButton() => Application.Quit();

        public void StartButton()
        {
            CinemachineCameraHelper.ChangeCamera(_defaultCamera);
            StartCoroutine(HideMenu());
            _mouseClickOnObjectScript.enabled = true;
            _moveCameraOnMouseButtonPressScript.StartMovement();
        }

        private IEnumerator HideMenu()
        {
            yield return new WaitForSeconds(0.5f);
            gameObject.SetActive(false);
        }
    }
}
