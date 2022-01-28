using UnityEngine;
using UnityEngine.Events;

namespace Interactable_object
{
    public class KeyholeAction : MonoBehaviour
    {
        [SerializeField] private UnityAction _onOpenLock;

        public void RaiseAction() => _onOpenLock?.Invoke();
    }
}
