using System;
using UnityEngine;

namespace Interactable_object
{
    public class ButtonObject : InteractableObject
    {
        [SerializeField] private Vector3 _endPosition;

        private Vector3 _startPosition;
        
        private void Awake()
        {
            _mainCamera = Camera.main;
            _startPosition = transform.position;
            _endPosition = transform.parent.TransformPoint(_endPosition);
        }

        public override void StartInteraction(Vector3 startPos = default)
        {
            isActive = true;
            transform.position = _endPosition;
        }

        public override void StopInteraction()
        {
            transform.position = _startPosition;
            _actionOnComplete?.Invoke();
            Destroy(this);
        }
    }
}
