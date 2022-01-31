using UnityEngine;
using UnityEngine.Events;

namespace Interactable_object
{
    public class KeyholeAction : MonoBehaviour
    {
        [SerializeField] private UnityEvent _onOpenLock;

        public void RaiseAction() => _onOpenLock?.Invoke();
    }
}
