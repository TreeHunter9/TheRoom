using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Utilities;

namespace Interactable_object
{
    [RequireComponent(typeof(PossiblePositions))]
    public class RotatableObject : InteractableObject
    {
        [SerializeField] private bool _simpleRotation;
        [SerializeField] private Vector3 _mouseXRotationAxis;
        [SerializeField] private Vector3 _mouseYRotationAxis;

        [Header("Rotation Range")] 
        [SerializeField] private bool _wrap;
        [SerializeField] private Vector3 _fromRotation;
        [SerializeField] private Vector3 _toRotation;
        [Space] 
        [SerializeField] private bool _stopWhenOnPosition;
        [Space]
        [Tooltip("Set axis to 1 if this object will rotate on this axis")]
        [SerializeField] private Vector3Int _rotationOnAxis;

        private PossiblePositions _possiblePositions;

        private Quaternion _startRotation;

        private GameObject _lookAtGO;
        private GameObject _planeForRaycast;

        private bool _isUsingForKey;

        private void Awake()
        {
            _mainCamera = Camera.main;
            _startRotation = transform.rotation;
            _rotationOnAxis = _rotationOnAxis.Invert();
            
            CreateGOForLookAt();
            CreatePlaneForRaycast();
            
            _rotationOnAxis = _rotationOnAxis.Invert();

            _isUsingForKey = TryGetComponent(out KeyController keyController);
            _possiblePositions = GetComponent<PossiblePositions>();
        }

        private void Update()
        {
            if (isActive == true)
            {
                if (_simpleRotation == true)
                    RotateObjectSimple();
                else
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

        private void RotateObjectSimple()
        {
            float rotX = Input.GetAxis("Mouse X");
            float rotY = Input.GetAxis("Mouse Y");
            float speedRotation = 4f;

            Vector3 angle = (_mouseXRotationAxis * -rotX + _mouseYRotationAxis * rotY) * speedRotation;
            //transform.Rotate(angle);
            Quaternion newRotation = transform.localRotation * Quaternion.Euler(angle);
            transform.localRotation = newRotation.Restrict(_fromRotation, _toRotation);
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
                direction = hit.point - _lookAtGO.transform.position;
                lookRotation = FindLookRotation(_rotationOnAxis, direction);
            }
            else
            {
                direction = _mainCamera.transform.position + ray.direction * 1000f - _lookAtGO.transform.position;
                lookRotation = FindLookRotation(_rotationOnAxis, direction);
            }
            print(lookRotation);
            lookRotation.x = _rotationOnAxis.x == 1 ? lookRotation.x :_startRotation.eulerAngles.x;
            lookRotation.y = _rotationOnAxis.y == 1 ? lookRotation.y :_startRotation.eulerAngles.y;
            lookRotation.z = _rotationOnAxis.z == 1 ? lookRotation.z :_startRotation.eulerAngles.z;
            return lookRotation;
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
            if (_isUsingForKey == false && _stopWhenOnPosition == false 
                                        && _possiblePositions.TryCheckPositions(out var key))
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
    }
}
