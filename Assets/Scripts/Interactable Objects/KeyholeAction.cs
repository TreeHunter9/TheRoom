using UnityEngine;
using UnityEngine.Events;

namespace TheRoom.InteractableObjects
{
    public class KeyholeAction : MonoBehaviour
    {
        [SerializeField] private UnityEvent _onOpenLock;

        public void RaiseAction() => _onOpenLock?.Invoke();
    }
}
