using System;
using System.Linq;
using UnityEngine;

namespace TheRoom.InteractableObjects
{
    public class StateOfInventoryItem : MonoBehaviour
    {
        private PossiblePositions[] _possiblePositionsScripts;

        private void Awake()
        {
            _possiblePositionsScripts = GetComponentsInChildren<PossiblePositions>();
        }

        public bool Compare(Pair<int, int>[] pairs)
        {
            for (int i = 0; i < _possiblePositionsScripts.Length; i++)
            {
                for (int j = 0; j < pairs.Length; j++)
                {
                    if (_possiblePositionsScripts[i].GetCurrentPositionPair().key != pairs[j].key)
                        continue;
                    if (_possiblePositionsScripts[i].GetCurrentPositionPair().value != pairs[j].value)
                        return false;
                    break;
                }
            }
            
            return true;
        }
    }
}
