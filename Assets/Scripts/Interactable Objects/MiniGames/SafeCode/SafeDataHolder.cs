using System;
using System.Collections.Generic;
using UnityEngine;

namespace TheRoom.InteractableObjects.MiniGames.SafeCode
{
    public class SafeDataHolder : MonoBehaviour
    {
        [SerializeField] private List<int> _activateOrder;
        public Transform arrowTransform;

        public event Action onComplete;
        public event Action onFailed;

        private int _currentActivationsCount = 0;

        public bool CheckCondition(int id)
        {
            if (id == _activateOrder[_currentActivationsCount])
            {
                _currentActivationsCount++;
                if (_currentActivationsCount == _activateOrder.Count)
                    onComplete?.Invoke();
                return true;
            }
            onFailed?.Invoke();
            _currentActivationsCount = 0;
            return false;
        }
    }
}
