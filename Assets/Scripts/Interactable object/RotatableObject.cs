using UnityEngine;

namespace Interactable_object
{
    public class RotatableObject : InteractableObject
    {
        [SerializeField] private Quaternion _endRotation;

        private Quaternion _startRotation;
        private Vector3 _offset;
        private Vector3 _startPosition;

        private Vector3 _mouseVector = Vector3.zero;

        private GameObject _lookAtGO;
        private GameObject _planeForRaycast;

        private void Awake()
        {
            _lookAtGO = new GameObject("ForRotationGO");
            _lookAtGO.transform.position = transform.position;
            _lookAtGO.transform.parent = transform.parent;
            
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

        private void CreatePlaneForRaycast()
        {
            _planeForRaycast = new GameObject("PlaneForRaycast");
            _planeForRaycast.AddComponent<BoxCollider>().size = new Vector3(100000, 0, 100000);
            _planeForRaycast.transform.position = transform.position;
            _planeForRaycast.transform.rotation = transform.rotation;
            _planeForRaycast.layer = Utilities.Constants.PlaneLayer;
            _planeForRaycast.SetActive(false);
        }

        /*private void RotateObject()
        {
            _mouseVector = GetMouseWorldPosition() + _offset;
            Vector3 direction = _mouseVector - transform.position;
            Vector3 v = Quaternion.LookRotation(direction, Vector3.up).eulerAngles;
            v.x = _startRotation.eulerAngles.x;
            transform.rotation = Quaternion.Euler(v);
        }*/
        
        private void RotateObject()
        {
            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 1000000f, (1 << Utilities.Constants.PlaneLayer)))
            {
                Vector3 direction = hit.point - _lookAtGO.transform.position;
                Vector3 lookRotation = Quaternion.LookRotation(direction, Vector3.up).eulerAngles;
                lookRotation.x = _startRotation.eulerAngles.x;
                _lookAtGO.transform.rotation = Quaternion.RotateTowards( 
                    _lookAtGO.transform.rotation, Quaternion.Euler(lookRotation), 4);
            }
            else
            {
                _mouseVector = GetMouseWorldPosition() + _offset;
                Vector3 direction = _mouseVector - _lookAtGO.transform.position;
                Vector3 v = Quaternion.LookRotation(direction, Vector3.up).eulerAngles;
                v.x = _startRotation.eulerAngles.x;
                _lookAtGO.transform.rotation = Quaternion.Euler(v);
            }
            print(Quaternion.Angle(_endRotation, transform.rotation));
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
            
            _startPosition = startPos;
            _offset = _startPosition - GetMouseWorldPosition();
            
            RotateObject();
            transform.parent = _lookAtGO.transform;
            _planeForRaycast.SetActive(true);
            
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
