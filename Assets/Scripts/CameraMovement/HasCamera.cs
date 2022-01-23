using Cinemachine;
using UnityEngine;
using Interactable_object;

namespace CameraMovement
{
    public class HasCamera : MonoBehaviour
    {
        [SerializeField] private CinemachineFreeLook _camera;

        private CinemachineBrain _cinemachineBrain;

        private Collider _collider;
        private InteractList _interactList;

        private void Awake()
        {
            _cinemachineBrain = Camera.main.GetComponent<CinemachineBrain>();
            _interactList = GetComponent<InteractList>();
            _collider = GetComponent<Collider>();
        }

        public void ChangeCameraView()
        {
            EnableInteractableObjects();
            
            _cinemachineBrain.ActiveVirtualCamera.Priority = 0;
            _camera.Priority = 1;
        }

        public void DisableInteractableObjects()
        {
            _collider.enabled = true;
            _interactList?.DisableInteractableObjects();
        }
        
        public void EnableInteractableObjects()
        {
            _collider.enabled = false;
            _interactList?.EnableInteractableObjects();
        }
    }
}
