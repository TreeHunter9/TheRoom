using System;
using System.Collections;
using System.Collections.Generic;
using TheRoom.InteractableObjects.MiniGames.OrderButtons;
using UnityEngine;
using UnityEngine.Events;

namespace TheRoom
{
    public class ButtonsController : MonoBehaviour
    {
        [SerializeField] private List<OrderButton> _orderButtons;
        [SerializeField] private List<int> _rightIds;
        [SerializeField] private UnityEvent _actionsOnComplete;
        
        private List<int> _currentIds;
        private int _rightButtonsCount;
        private int _currentButtonsCount = 0;

        private void Awake()
        {
            _rightButtonsCount = _rightIds.Count;
            _currentIds = new List<int>(_rightButtonsCount);
            
            foreach (OrderButton orderButton in _orderButtons)
            {
                orderButton.onButtonPress += ConditionCheck;
            }
        }

        private void ConditionCheck(int id, StateOfActivity state)
        {
            if (state == StateOfActivity.Active)
            {
                _currentIds.Add(id);
                _currentButtonsCount++;
            }
            else
            {
                _currentIds.Remove(id);
                _currentButtonsCount--;
            }

            if (_currentButtonsCount != _rightButtonsCount) 
                return;
            foreach (var rightId in _rightIds)
            {
                if (_currentIds.Contains(rightId) == true) 
                    continue;
                Refresh();
                return;
            }
            _actionsOnComplete?.Invoke();
            Complete();
        }

        private void Refresh()
        {
            _currentIds.Clear();
            _currentButtonsCount = 0;
            foreach (OrderButton orderButton in _orderButtons)
            {
                orderButton.Refresh();
            }
        }

        private void Complete()
        {
            foreach (OrderButton orderButton in _orderButtons)
            {
                orderButton.Destroy();
            }
            Destroy(this);
        }
    }
}
