using System;
using System.Collections.Generic;
using TheRoom.InventorySystem.Core;
using TheRoom.Utilities;
using UnityEngine;

namespace TheRoom.InteractableObjects
{
    [Serializable]
    public struct Pair<T1, T2>
    {
        public T1 key;
        public T2 value;

        public Pair(T1 key, T2 value)
        {
            this.key = key;
            this.value = value;
        }
    }
    
    public class ItemNeeded : MonoBehaviour
    {
        [SerializeField] private InventoryItemType _key;
        [Tooltip("KEY - id объекта в скрипте PossiblePositins, VALUE - нужный id позиции")]
        [SerializeField] private Pair<int, int>[] _objectPositionsPair;
        
        [Header("Position and Rotation for Item")] 
        [SerializeField] private Transform _keyTransform;

        public InventoryItemType NeededItemType => _key;

        public Pair<int, int>[] GetPositionsPair() => _objectPositionsPair;

        public bool IsSimpleItem() => _objectPositionsPair.Length == 0 ? true : false;

        public void Enable() => gameObject.tag = Constants.NeededItemTag;

        public void SetupItem()
        {
            GameObject keyGO = Instantiate(_key.gameObjectForInteraction, _keyTransform.position, 
                _keyTransform.rotation, transform);
            keyGO.transform.localScale = _keyTransform.localScale;
        }
    }
}
