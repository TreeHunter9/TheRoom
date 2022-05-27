using Cinemachine;
using TheRoom.InteractableObjects;
using UnityEngine;
using UnityEngine.Events;

namespace TheRoom.CameraMovement
{
    [RequireComponent(typeof(InteractList))]
    public class HasCamera : MonoBehaviour
    {
        [SerializeField] private CinemachineFreeLook _camera;
        [SerializeField] private UnityEvent _actionOnCameraLive;

        private Collider _collider;
        private InteractList _interactList;

        private bool _hasCollider;

        private void Awake()
        {
            _interactList = GetComponent<InteractList>();
            _hasCollider = TryGetComponent(out _collider);
        }

        public void ChangeCameraView() => CinemachineCameraHelper.ChangeCamera(_camera);

        public void DisableInteractableObjects()
        {
            if (_hasCollider == true)
                _collider.enabled = true;
            _interactList.DisableObjects();
        }

        public void EnableInteractableObjects()
        {
            if (_hasCollider == true)
                _collider.enabled = false;
            _interactList.EnableObjects();
            _actionOnCameraLive?.Invoke();
        }

        public void DestroyScript() => Destroy(this);
    }
}
