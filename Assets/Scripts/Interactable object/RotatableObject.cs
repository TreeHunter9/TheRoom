using System;
using UnityEngine;
using Utilities;

namespace Interactable_object
{
    public class RotatableObject : InteractableObject
    {
        [SerializeField] private Quaternion _endRotation;
        [Tooltip("Set axis to 1 if this object will rotate on this axis")]
        [SerializeField] private Vector3Int _rotationOnAxis;

        private Quaternion _startRotation;
        private Vector3 _offset;
        private Vector3 _startPosition;

        private Vector3 _mouseVector = Vector3.zero;

        private GameObject _lookAtGO;
        private GameObject _planeForRaycast;

        private void Awake()
        {
            _mainCamera = Camera.main;
            _startRotation = transform.rotation;
            _rotationOnAxis = _rotationOnAxis.Invert();
            
            CreateGOForLookAt();
            CreatePlaneForRaycast();
            
            _rotationOnAxis = _rotationOnAxis.Invert();
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
            _lookAtGO.transform.rotation = Quaternion.RotateTowards( 
                _lookAtGO.transform.rotation, Quaternion.Euler(lookRotation), 3);
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
                _mouseVector = hit.point;
                direction = hit.point - _lookAtGO.transform.position;
                lookRotation = FindLookRotation(_rotationOnAxis, direction);
            }
            else
            {
                _mouseVector = GetMouseWorldPosition() + _offset;
                direction = _mainCamera.transform.position + ray.direction * 1000f - _lookAtGO.transform.position;
                lookRotation = FindLookRotation(_rotationOnAxis, direction);
            }
            print(lookRotation);
            lookRotation.x = _rotationOnAxis.x == 1 ? lookRotation.x :_startRotation.eulerAngles.x;
            lookRotation.y = _rotationOnAxis.y == 1 ? lookRotation.y :_startRotation.eulerAngles.y;
            lookRotation.z = _rotationOnAxis.z == 1 ? lookRotation.z :_startRotation.eulerAngles.z;
            return lookRotation;
        }
        
        private Vector3 GetMouseWorldPosition()
        {
            Vector3 mousePoint = Input.mousePosition;
            mousePoint.z = _mainCamera.WorldToScreenPoint(_startPosition).z;
            return _mainCamera.ScreenToWorldPoint(mousePoint);
        }

        private Vector3 FindLookRotation(Vector3Int vector3Int, Vector3 direction)
        {
            if (vector3Int.x == 1)
            {
                return Quaternion.LookRotation(direction, Vector3.up).eulerAngles;
            }
            else if (vector3Int.y == 1)
            {
                return Quaternion.LookRotation(direction, Vector3.up).eulerAngles;
            }
            else
            {
                return Quaternion.LookRotation(Vector3.forward, direction).eulerAngles;
            }
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
