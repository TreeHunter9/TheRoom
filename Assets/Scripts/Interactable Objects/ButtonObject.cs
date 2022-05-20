using System;
using UnityEngine;

namespace TheRoom.InteractableObjects
{
    public class ButtonObject : InteractableObject
    {
        [SerializeField] private Vector3 _endPosition;
        [SerializeField] private bool _destroyOnRelease = true;

        private Vector3 _startPosition;
        
        public bool isEnabled = true;
        public event Action onButtonRelease;
        
        private void Awake()
        {
            _startPosition = transform.localPosition;
            _endPosition = transform.parent.TransformPoint(_endPosition);
        }

        public override void StartInteraction(Vector3 startPos = default)
        {
            if (isEnabled == false)
                return;
            isActive = true;
            transform.position = _endPosition;
        }

        public override void StopInteraction()
        {
            transform.localPosition = _startPosition;
            _actionOnComplete?.Invoke();
            onButtonRelease?.Invoke();
            if (_destroyOnRelease == true)
                Destroy(this);
            else
                isEnabled = false;
        }
    }
}
