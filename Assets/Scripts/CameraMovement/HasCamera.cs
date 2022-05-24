using Cinemachine;
using TheRoom.InteractableObjects;
using UnityEngine;

namespace TheRoom.CameraMovement
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
            print(_interactList);
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
            print(transform);
            _interactList.EnableObjects();
        }
    }
}
