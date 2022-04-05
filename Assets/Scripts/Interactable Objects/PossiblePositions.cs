using System;
using UnityEngine;

namespace TheRoom.InteractableObjects
{
    [Serializable]
    public struct Positions
    {
        public int key;
        public Quaternion value;
    }
    
    public class PossiblePositions : MonoBehaviour
    {
        [SerializeField] private int _objectID;
        [SerializeField] private Positions[] _positions;

        private Pair<int, int> _currentPositionPair;

        private void Awake()
        {
            _currentPositionPair = new Pair<int, int>(_objectID, -1);
        }

        public bool TryCheckPositions(bool isSimpleRotation)
        {
            foreach (var pos in _positions)
            {
                Quaternion currentRotation = isSimpleRotation
                    ? transform.localRotation
                    : transform.parent.rotation * transform.localRotation;
                if (Quaternion.Angle(currentRotation, pos.value) <= 4.5f)
                {
                    if (isSimpleRotation == true)
                        transform.localRotation = pos.value;
                    else
                        transform.rotation = pos.value;
                    _currentPositionPair.value = pos.key;
                    return true;
                }
            }

            _currentPositionPair.value = -1;
            return false;
        }

        public Pair<int, int> GetCurrentPositionPair() => _currentPositionPair;
    }
}