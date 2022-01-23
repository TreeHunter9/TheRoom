using UnityEngine;
using UnityEngine.Events;

namespace Interactable_object
{
    public abstract class InteractableObject : MonoBehaviour
    {
        [SerializeField] protected UnityEvent _actionOnComplete;
        [SerializeField] private Vector3 FORSAVE;
        
        protected Camera _mainCamera;
        
        protected bool _isActive = false;

        public abstract void StartInteraction(Vector3 startPos = default);
        
        public abstract void StopInteraction();
    }
}
