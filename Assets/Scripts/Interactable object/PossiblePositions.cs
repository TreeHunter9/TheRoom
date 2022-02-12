using System;
using UnityEngine;

namespace Interactable_object
{
    [Serializable]
    public class PositionsDictionary
    {
        public int key;
        public Quaternion value;
    }
    
    public class PossiblePositions : MonoBehaviour
    {
        [SerializeField] private PositionsDictionary[] _positions;

        public bool TryCheckPositions(out int key)
        {
            foreach (var pos in _positions)
            {
                if (Quaternion.Angle(transform.parent.rotation * transform.localRotation, pos.value) <= 4.5f)
                {
                    transform.rotation = pos.value;
                    key = pos.key;
                    return true;
                }
            }

            key = -1;
            return false;
        }
    }
}
