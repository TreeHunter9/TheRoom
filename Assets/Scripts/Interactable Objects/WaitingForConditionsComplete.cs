using System;
using UnityEngine;
using UnityEngine.Events;

namespace TheRoom.InteractableObjects
{
    public class WaitingForConditionsComplete : MonoBehaviour
    {
        [SerializeField] private PossiblePositions[] _possiblePositionsScripts;
        [SerializeField] private Pair<int, int>[] _expectedPossitionsPair;
        [SerializeField] private UnityEvent _onConditionsComplete;

        private void Awake()
        {
            foreach (PossiblePositions possiblePositionsScript in _possiblePositionsScripts)
            {
                possiblePositionsScript.onPosition += CheckPositions;
            }
        }

        private void OnDestroy()
        {
            foreach (PossiblePositions possiblePositionsScript in _possiblePositionsScripts)
            {
                possiblePositionsScript.onPosition -= CheckPositions;
                Destroy(possiblePositionsScript.GetComponent<RotatableObject>());
            }
        }

        private void CheckPositions()
        {
            for (int i = 0; i < _possiblePositionsScripts.Length; i++)
            {
                for (int j = 0; j < _expectedPossitionsPair.Length; j++)
                {
                    if (_possiblePositionsScripts[i].GetCurrentPositionPair().key != _expectedPossitionsPair[j].key) 
                        continue;
                    if (_possiblePositionsScripts[i].GetCurrentPositionPair().value != _expectedPossitionsPair[j].value) 
                        return;
                    break;
                }
            }
            _onConditionsComplete?.Invoke();
            Destroy(this);
        }
    }
}