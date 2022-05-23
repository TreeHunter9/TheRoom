using System;
using UnityEngine;

namespace TheRoom.InteractableObjects
{
    public class MovableObject : InteractableObject
    {
        [SerializeField] private Vector3 _endPosition;
        [Range(0f, 1f)] 
        [SerializeField] private float _percentageForMagnet = 0.8f;

        [SerializeField] private bool _onlyMoving;

        private Vector3 _startPosition;

        private Vector3 _mouseStartPosition;

        private Vector3 _offset;

        private float _completePercent = 0f;
        private float CompletePercent
        {
            get => _completePercent;
            set => _completePercent = Mathf.Clamp(value, 0f, 1f);
        }

        private void Awake()
        {
            _mainCamera = Camera.main;
            _startPosition = transform.position;
            //_endPosition = transform.parent.TransformPoint(_endPosition);
        }

        private void OnEnable()
        {
            _endPosition = transform.parent.TransformPoint(_endPosition);
        }

        private void LateUpdate()
        {
            if (isActive == true)
            {
                MoveObject();
            }
        }

        private void MoveObject()
        {
            Vector3 mouseVector = GetMouseWorldPosition() + _offset;
            CompletePercent = FindTForLerp(mouseVector);
            transform.position = Vector3.Lerp(_startPosition, _endPosition, CompletePercent);
        }

        private Vector3 GetMouseWorldPosition()
        {
            Vector3 mousePoint = Input.mousePosition;
            mousePoint.z = _mainCamera.WorldToScreenPoint(transform.position).z;
            return _mainCamera.ScreenToWorldPoint(mousePoint);
        }

        private float FindTForLerp(Vector3 mouseVector)
        {
            Vector3 projection = Vector3.Project(mouseVector - _mouseStartPosition,
                _endPosition - _startPosition);
            
            float x_t = projection.x / (_endPosition.x - _startPosition.x);
            float y_t = projection.y / (_endPosition.y - _startPosition.y);
            float z_t = projection.z / (_endPosition.z - _startPosition.z);
            
            x_t = Mathf.Clamp01(x_t);
            y_t = Mathf.Clamp01(y_t);
            z_t = Mathf.Clamp01(z_t);

            int axisCount = 3;

            float t = 0f;
            t += CheckForNaN(x_t, ref axisCount);
            t += CheckForNaN(y_t, ref axisCount);
            t += CheckForNaN(z_t, ref axisCount);
            t /= axisCount;

            return t;
        }

        private float CheckForNaN(float axisValue, ref int axisCount)
        {
            if (float.IsNaN(axisValue) == false)
                return axisValue;

            axisCount--;
            return 0f;
        }

        public override void StartInteraction(Vector3 startPos = default)
        {
            _mouseStartPosition = _startPosition;
            _offset = transform.position - GetMouseWorldPosition();
            isActive = true;
        }

        public override void StopInteraction()
        {
            isActive = false;
            
            if (_onlyMoving == true)
                return;
            
            if (CompletePercent >= _percentageForMagnet)
            {
                CompletePercent = 1f;
                transform.position = _endPosition;
                
                _actionOnComplete?.Invoke();
                Destroy(this);
            }
        }
    }
}
