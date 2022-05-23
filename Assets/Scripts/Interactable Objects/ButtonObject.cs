using System;
using UnityEngine;

namespace TheRoom.InteractableObjects
{
    public class ButtonObject : InteractableObject
    {
        [SerializeField] private Vector3 _endPosition;
        [SerializeField] private bool _destroyOnRelease = true;

        private Vector3 _startPosition;

        private Collider _collider;
        public event Action onButtonRelease;
        
        private void Awake()
        {
            _startPosition = transform.localPosition;
            _endPosition = transform.parent.TransformPoint(_endPosition);
            _collider = GetComponent<Collider>();
        }

        public override void StartInteraction(Vector3 startPos = default)
        {
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
                _collider.enabled = false;
        }

        public void SetActive(bool active)
        {
            if (_collider != null)
                _collider.enabled = active;
        }

        public void RemoveCollide() => Destroy(_collider);
    }
}
