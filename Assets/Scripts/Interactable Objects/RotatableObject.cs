using System;
using TheRoom.Utilities;
using UnityEditor;
using UnityEngine;

namespace TheRoom.InteractableObjects
{
    [RequireComponent(typeof(PossiblePositions))]
    public class RotatableObject : InteractableObject
    {
        [SerializeField] private bool _simpleRotation;
        [SerializeField] private Vector3 _mouseXRotationAxis;
        [SerializeField] private Vector3 _mouseYRotationAxis;

        [Header("Rotation Range")] 
        [SerializeField] private bool _wrap = true;
        [SerializeField] private Vector3 _minRotation;
        [SerializeField] private Vector3 _maxRotation;
        [Space] 
        [SerializeField] private bool _stopWhenOnPosition;
        [Space]
        [Tooltip("Set axis to 1 if this object will rotate on this axis")]
        [SerializeField] private Vector3Int _rotationOnAxis;
        [SerializeField] private float _speedRotation = 300f;

        [Tooltip("Если будет вращаться в противоположную сторону")]
        [SerializeField] private bool _invertZ;
        [SerializeField] private bool _invertY;
        [SerializeField] private bool _invertX;

        [SerializeField] private bool _changeRotationX;
        [SerializeField] private bool _changeRotationY;
        [SerializeField] private bool _changeRotationZ;

        private PossiblePositions _possiblePositions;

        private Quaternion _startRotation;

        private GameObject _lookAtGO;
        private GameObject _planeForRaycast;

        private bool _isUsingForKey;


        private void Awake()
        {
            _mainCamera = Camera.main;
            _startRotation = transform.rotation;
            _isUsingForKey = TryGetComponent(out KeyController keyController);
            _possiblePositions = GetComponent<PossiblePositions>();
        }

        private void Start()
        {
            _rotationOnAxis = _rotationOnAxis.Invert();
            
            CreateGOForLookAt();
            CreatePlaneForRaycast();

            _rotationOnAxis = _rotationOnAxis.Invert();
        }

        private void LateUpdate()
        {
            if (isActive == false) 
                return;
            
            if (_simpleRotation == true)
                RotateObjectSimple();
            else
                RotateObject();
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
            _planeForRaycast.AddComponent<BoxCollider>().size = new Vector3(
                _rotationOnAxis.x * 1000f, _rotationOnAxis.y * 1000f, _rotationOnAxis.z * 1000f);
            _planeForRaycast.transform.position = transform.position;
            _planeForRaycast.transform.rotation = transform.rotation;
            _planeForRaycast.layer = Constants.PlaneLayer;
            _planeForRaycast.SetActive(false);
        }

        private void RotateObject()
        {
            Vector3 lookRotation = FindRotation();
            Quaternion newRotation = Quaternion.RotateTowards(_lookAtGO.transform.rotation,
                Quaternion.Euler(lookRotation), _speedRotation * Time.deltaTime);
            //print(newRotation.eulerAngles);
            _lookAtGO.transform.rotation = _wrap
                ? newRotation
                : newRotation.Restrict(_minRotation, _maxRotation);
        }

        private void RotateObjectSimple()
        {
            float rotX = Input.GetAxis("Mouse X");
            float rotY = Input.GetAxis("Mouse Y");
            float speedRotation = 4f;

            Vector3 angle = (_mouseXRotationAxis * -rotX + _mouseYRotationAxis * rotY) * speedRotation;
            //transform.Rotate(angle);
            Quaternion newRotation = transform.localRotation * Quaternion.Euler(angle);
            transform.localRotation = _wrap ? newRotation : newRotation.Restrict(_minRotation, _maxRotation);
        }

        private void RotateGOForLookAt()
        {
            Vector3 lookRotation = FindRotation();
            _lookAtGO.transform.rotation = Quaternion.Euler(lookRotation);
        }

        private Vector3 FindRotation()
        {
            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            Vector3 direction, lookRotation;
            if (Physics.Raycast(ray, out RaycastHit hit, 1000f, 1 << Constants.PlaneLayer))
            {
                Vector3 localHit = _lookAtGO.transform.parent.InverseTransformPoint(hit.point);
                direction = (localHit - _lookAtGO.transform.localPosition).normalized;
            }
            else
            {
                direction = _mainCamera.transform.position + ray.direction * 1000f - _lookAtGO.transform.position;
            }
            lookRotation.x = _rotationOnAxis.x == 1 ? FindLookRotationX(direction) :_startRotation.eulerAngles.x;
            lookRotation.y = _rotationOnAxis.y == 1 ? FindLookRotationY(direction) :_startRotation.eulerAngles.y;
            lookRotation.z = _rotationOnAxis.z == 1 ? FindLookRotationZ(direction) :_startRotation.eulerAngles.z;
            return lookRotation;
        }
        
        //TODO: отрефакторить!!!
        private float FindLookRotationX(Vector3 direction)
        {
            if (_changeRotationX == true)
            {
                return _invertX
                    ? Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg
                    : Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg;
            }
            return _invertX
                ? Mathf.Atan2(direction.y, direction.z) * Mathf.Rad2Deg
                : Mathf.Atan2(direction.z, direction.y) * Mathf.Rad2Deg;
        }

        private float FindLookRotationY(Vector3 direction)
        {
            if (_changeRotationY == true)
            {
                return _invertY
                    ? Mathf.Atan2(direction.y, direction.z) * Mathf.Rad2Deg
                    : Mathf.Atan2(direction.z, direction.y) * Mathf.Rad2Deg;
            }
            return _invertY
                ? Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg
                : Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        }

        private float FindLookRotationZ(Vector3 direction)
        {
            if (_changeRotationZ == true)
            {
                return _invertZ
                    ? Mathf.Atan2(direction.y, direction.z) * Mathf.Rad2Deg
                    : Mathf.Atan2(direction.z, direction.y) * Mathf.Rad2Deg;
            }
            return _invertZ
                ? Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg
                : Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
        }

        public override void StartInteraction(Vector3 startPos = default)
        {
            if (_simpleRotation == false)
            {
                _planeForRaycast.transform.position = startPos;
                _planeForRaycast.SetActive(true);

                RotateGOForLookAt();
                transform.parent = _lookAtGO.transform;
            }

            isActive = true;
        }

        public override void StopInteraction()
        {
            bool isOnPosition = _possiblePositions.TryCheckPositions(_simpleRotation);
            if (_isUsingForKey == false && _stopWhenOnPosition == true 
                                        && isOnPosition == true)
            {
                _actionOnComplete?.Invoke();
                Destroy(this);
            }
            if (_simpleRotation == false)
            {
                transform.parent = _lookAtGO.transform.parent;
                _planeForRaycast.SetActive(false);
            }
            isActive = false;
        }

        public Vector3 GetAxis() => _rotationOnAxis;
    }
}
