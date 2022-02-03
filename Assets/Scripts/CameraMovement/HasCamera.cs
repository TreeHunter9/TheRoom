using Cinemachine;
using UnityEngine;
using Interactable_object;

namespace CameraMovement
{
    [RequireComponent(typeof(InteractList))]
    public class HasCamera : MonoBehaviour
    {
        [SerializeField] private CinemachineFreeLook _camera;

        private Collider _collider;
        private InteractList _interactList;

        private bool _hasCollider;

        private void Awake()
        {
            _interactList = GetComponent<InteractList>();
            _hasCollider = TryGetComponent(out _collider);
        }

        public void ChangeCameraView()
        {
            EnableInteractableObjects();
            
            CameraChanger.ChangeCamera(_camera);
        }

        public void DisableInteractableObjects()
        {
            if (_hasCollider == true)
                _collider.enabled = true;
            _interactList?.DisableInteractableObjects();
        }

        private void EnableInteractableObjects()
        {
            if (_hasCollider == true)
                _collider.enabled = false;
            _interactList?.EnableInteractableObjects();
        }
    }
}
