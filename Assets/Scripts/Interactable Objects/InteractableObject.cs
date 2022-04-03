using System;
using UnityEngine;
using UnityEngine.Events;

namespace TheRoom.InteractableObjects
{
    public abstract class InteractableObject : MonoBehaviour
    {
        [SerializeField] protected UnityEvent _actionOnComplete;
        [SerializeField] private Vector3 FORSAVE;
        
        protected Camera _mainCamera;
        
        private bool _isActive = false;

        protected bool isActive
        {
            get => _isActive;
            set
            {
                _isActive = value;
                onChangeActive?.Invoke(_isActive);
            }
        }

        public event Action<bool> onChangeActive;

        public abstract void StartInteraction(Vector3 startPos = default);
        
        public abstract void StopInteraction();
    }
}
