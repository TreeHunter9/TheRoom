using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace TheRoom.InteractableObjects.MiniGames.SafeCode
{
    public class SafeDataHolder : MonoBehaviour
    {
        [SerializeField] private List<int> _activateOrder;
        [SerializeField] public UnityEvent _actionsOnComplete;
        public Transform arrowTransform;

        private RotatableObject _rotatableObjectScript;

        public event Action<bool> onActiveChange;
        public event Action onComplete;
        public event Action onFailed;

        private int _currentActivationsCount = 0;

        private void Awake()
        {
            _rotatableObjectScript = GetComponent<RotatableObject>();
            _rotatableObjectScript.onChangeActive += RaiseActiveChange;
        }

        private void OnDisable()
        {
            _rotatableObjectScript.onChangeActive -= RaiseActiveChange;
        }

        public bool CheckCondition(int id)
        {
            if (id == _activateOrder[_currentActivationsCount])
            {
                _currentActivationsCount++;
                if (_currentActivationsCount == _activateOrder.Count)
                {
                    onComplete?.Invoke();
                    _actionsOnComplete?.Invoke();
                    _rotatableObjectScript.StopInteraction();
                    Destroy(_rotatableObjectScript);
                }
                return true;
            }
            onFailed?.Invoke();
            _currentActivationsCount = 0;
            return false;
        }

        private void RaiseActiveChange(bool active) => onActiveChange?.Invoke(active);
    }
}
