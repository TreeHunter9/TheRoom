using UnityEngine;

namespace Interactable_object
{
    public class RotatableObject : InteractableObject
    {
        [SerializeField] private Quaternion _endRotation;

        private Quaternion _startRotation;
        private Vector3 _offset;
        private Vector3 _startPosition;

        private Vector3 _mouseVector;

        private GameObject _lookAtGO;
        private GameObject _planeForRaycast;

        private void Awake()
        {
            CreateGOForLookAt();
            CreatePlaneForRaycast();

            _mainCamera = Camera.main;
            _startRotation = transform.rotation;
        }

        private void Update()
        {
            if (_isActive == true)
            {
                RotateObject();
            }
        }

        private void CreateGOForLookAt()
        {
            _lookAtGO = new GameObject("ForRotationGO");
            _lookAtGO.transform.position = transform.position;
            _lookAtGO.transform.parent = transform.parent;
        }

        private void CreatePlaneForRaycast()
        {
            _planeForRaycast = new GameObject("PlaneForRaycast");
            _planeForRaycast.AddComponent<BoxCollider>().size = new Vector3(1000f, 0f, 1000f);
            _planeForRaycast.transform.position = transform.position;
            _planeForRaycast.transform.rotation = transform.rotation;
            _planeForRaycast.layer = Utilities.Constants.PlaneLayer;
            _planeForRaycast.SetActive(false);
        }

        private void RotateObject()
        {
            Vector3 lookRotation = FindLookRotation();
            _lookAtGO.transform.rotation = Quaternion.RotateTowards( 
                _lookAtGO.transform.rotation, Quaternion.Euler(lookRotation), 3);
        }

        private void RotateGOForLookAt()
        {
            Vector3 lookRotation = FindLookRotation();
            _lookAtGO.transform.rotation = Quaternion.Euler(lookRotation);
        }

        private Vector3 FindLookRotation()
        {
            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            Vector3 direction, lookRotation;
            if (Physics.Raycast(ray, out RaycastHit hit, 1000f, 1 << Utilities.Constants.PlaneLayer))
            {
                direction = hit.point - _lookAtGO.transform.position;
                lookRotation = Quaternion.LookRotation(direction, Vector3.up).eulerAngles;
            }
            else
            {
                _mouseVector = GetMouseWorldPosition() + _offset;
                direction = _mainCamera.transform.position + ray.direction * 1000f - _lookAtGO.transform.position;
                lookRotation = Quaternion.LookRotation(direction, Vector3.up).eulerAngles;
            }
            lookRotation.x = _startRotation.eulerAngles.x;
            return lookRotation;
        }
        
        private Vector3 GetMouseWorldPosition()
        {
            Vector3 mousePoint = Input.mousePosition;
            mousePoint.z = _mainCamera.WorldToScreenPoint(_startPosition).z;
            return _mainCamera.ScreenToWorldPoint(mousePoint);
        }

        public override void StartInteraction(Vector3 startPos = default)
        {
            _planeForRaycast.transform.position = startPos;
            _planeForRaycast.SetActive(true);
            
            _startPosition = startPos;
            _offset = _startPosition - GetMouseWorldPosition();

            RotateGOForLookAt();
            transform.parent = _lookAtGO.transform;

            _isActive = true;
        }

        public override void StopInteraction()
        {
            if (Quaternion.Angle(_endRotation, transform.rotation) <= 4.5f)
            {
                transform.rotation = _endRotation;
                _actionOnComplete?.Invoke();
                
                Destroy(this);
            }
            transform.parent = _lookAtGO.transform.parent;
            _planeForRaycast.SetActive(false);
            _isActive = false;
        }
    }
}
