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
        [SerializeField] private float _allowedAngleForMagnit = 4.5f;
        [SerializeField] private Positions[] _positions;

        private Pair<int, int> _currentPositionPair;

        public event Action onPosition;  

        private void Awake()
        {
            _currentPositionPair = new Pair<int, int>(_objectID, -1);
            foreach (var pos in _positions)
            {
                Quaternion currentRotation = transform.localRotation;
                if (Quaternion.Angle(currentRotation, pos.value) <= _allowedAngleForMagnit)
                {
                    transform.localRotation = pos.value;
                    _currentPositionPair.value = pos.key;
                }
            }
        }

        public bool TryCheckPositions(bool isSimpleRotation)
        {
            foreach (var pos in _positions)
            {
                Quaternion currentRotation = isSimpleRotation
                    ? transform.localRotation
                    : transform.parent.localRotation * transform.localRotation;
                if (Quaternion.Angle(currentRotation, pos.value) <= _allowedAngleForMagnit)
                {
                    if (isSimpleRotation == true)
                        transform.localRotation = pos.value;
                    else
                    {
                        transform.localRotation = pos.value;
                        transform.parent.localRotation = Quaternion.identity;
                    }
                    _currentPositionPair.value = pos.key;
                    onPosition?.Invoke();
                    return true;
                }
            }

            _currentPositionPair.value = -1;
            return false;
        }

        public Pair<int, int> GetCurrentPositionPair() => _currentPositionPair;
    }
}
